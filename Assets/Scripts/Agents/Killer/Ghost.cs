using UnityEngine;

public class Ghost : AgentKiller
{
    [SerializeField] private KillerUI _killerUI;

    [Header("Patrol Stats")]
    [SerializeField] private PatrolData _patrolData;

    [Header("Chase Stats")]
    [SerializeField] private ChaseData _chaseData;

    [Header("Track Stats")]
    [SerializeField] private TrackData _trackData;

    protected override void Awake()
    {
        base.Awake();

        StatePatrol statePatrol = new StatePatrol(this, _killerUI, _patrolData, _stateMachine);
        StateChase stateChase = new StateChase(this, _killerUI, _chaseData, _stateMachine);
        StateTrack stateTrack = new StateTrack(this, _killerUI, _trackData, _stateMachine);

        _stateMachine.RegisterState(KillerMode.Patrol, statePatrol);
        _stateMachine.RegisterState(KillerMode.Chase, stateChase);
        _stateMachine.RegisterState(KillerMode.Track, stateTrack);

        _stateMachine.StartFirstState(KillerMode.Patrol);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }
}
