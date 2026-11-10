using UnityEngine;

public class Ghost : AgentKiller
{
    private KillerTrigger _killerTrigger;
    private KillerUI _killerUI;

    private StatePatrol _statePatrol;
    private StateChase _stateChase;
    private StateTrack _stateTrack;

    [Header("Patrol Stats")]
    [SerializeField] private PatrolData _patrolData;

    protected override void Awake()
    {
        base.Awake();

        _killerTrigger = GetComponentInChildren<KillerTrigger>();
        _killerUI = GetComponentInChildren<KillerUI>();

        _statePatrol = new StatePatrol(this, _killerUI, _patrolData, _stateMachine);
        _stateChase = new StateChase(this, _killerUI, _stateMachine);
        _stateTrack = new StateTrack(this, _killerUI, _stateMachine);

        _stateMachine.RegisterState(KillerMode.Patrol, _statePatrol);
        _stateMachine.RegisterState(KillerMode.Chase, _stateChase);
        _stateMachine.RegisterState(KillerMode.Track, _stateTrack);

        _stateMachine.StartFirstState(KillerMode.Patrol);
    }

    private void OnEnable()
    {
        if (_killerTrigger == null) return;

        _killerTrigger.OnHunterEnter += TargetEntered;
    }

    private void OnDisable()
    {
        if (_killerTrigger == null) return;

        _killerTrigger.OnHunterEnter -= TargetEntered;
    }

    private void Update()
    {
        if (CurrentTarget != null && _killerTrigger.CanSeeTarget && _stateMachine._currentState != _stateChase) _stateMachine.ChangeState(KillerMode.Chase);

        else if (CurrentTarget != null && !_killerTrigger.CanSeeTarget && _stateMachine._currentState != _stateTrack) _stateMachine.ChangeState(KillerMode.Track);

        else if (_stateMachine._currentState != _statePatrol) _stateMachine.ChangeState(KillerMode.Patrol);

        _stateMachine.Update();

        Movement();
    }
}
