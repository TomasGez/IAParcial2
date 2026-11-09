using UnityEngine;

public class Ghost : AgentKiller
{
    private KillerTrigger _killerTrigger;
    private KillerUI _killerUI;

    [Header("Patrol Stats")]
    [SerializeField] private PatrolData _patrolData;

    [Header("Chase Stats")]
    [SerializeField] private ChaseData _chaseData;

    [Header("Track Stats")]
    [SerializeField] private TrackData _trackData;

    protected override void Awake()
    {
        base.Awake();

        _killerTrigger = GetComponentInChildren<KillerTrigger>();
        _killerUI = GetComponentInChildren<KillerUI>();

        StatePatrol statePatrol = new StatePatrol(this, _killerUI, _patrolData, _stateMachine);
        StateChase stateChase = new StateChase(this, _killerUI, _chaseData, _stateMachine);
        StateTrack stateTrack = new StateTrack(this, _killerUI, _trackData, _stateMachine);

        _stateMachine.RegisterState(KillerMode.Patrol, statePatrol);
        _stateMachine.RegisterState(KillerMode.Chase, stateChase);
        _stateMachine.RegisterState(KillerMode.Track, stateTrack);

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
        if (CurrentTarget != null && _killerTrigger.CanSeeTarget) _stateMachine.ChangeState(KillerMode.Chase);

        else if (CurrentTarget != null && !_killerTrigger.CanSeeTarget) _stateMachine.ChangeState(KillerMode.Track);

        else _stateMachine.ChangeState(KillerMode.Patrol);

        _stateMachine.Update();

        Movement();
    }
}
