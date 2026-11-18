using UnityEngine;

public class Ghost : AgentKiller
{
    private KillerTrigger _killerTrigger;
    private KillerUI _killerUI;

    private StatePatrol _statePatrol;
    private StateChase _stateChase;
    private StateTrack _stateTrack;

    [SerializeField] private PatrolData _patrolData;

    protected override void Awake()
    {
        base.Awake();

        _killerTrigger = GetComponentInChildren<KillerTrigger>();
        _killerUI = GetComponentInChildren<KillerUI>();

        _statePatrol = new StatePatrol(this, _killerTrigger, _killerUI, _patrolData, _stateMachine);
        _stateChase = new StateChase(this, _killerTrigger, _killerUI, _stateMachine);
        _stateTrack = new StateTrack(this, _killerTrigger, _killerUI, _stateMachine);

        _stateMachine.RegisterState(KillerMode.Patrol, _statePatrol);
        _stateMachine.RegisterState(KillerMode.Chase, _stateChase);
        _stateMachine.RegisterState(KillerMode.Track, _stateTrack);

        _allGhost.Add(this);

        _stateMachine.StartFirstState(KillerMode.Patrol);
    }

    private void OnEnable()
    {
        if (_killerTrigger == null) return;

        _killerTrigger.OnHunterEnter += GhostWolfReference;
    }

    private void OnDisable()
    {
        if (_killerTrigger == null) return;

        _killerTrigger.OnHunterEnter -= GhostWolfReference;
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }
}
