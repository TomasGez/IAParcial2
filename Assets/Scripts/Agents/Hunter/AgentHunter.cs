using UnityEngine;

public class AgentHunter : AgentNPC
{
    [SerializeField] private HunterUI _hunterUI;

    [Header("Scout Stats")]
    [SerializeField] private ScoutData _scoutData;

    [Header("Retreat Stats")]
    [SerializeField] private RetreatData _retreatData;

    [Header("Rest Stats")]
    [SerializeField] private RestData _restData;

    [Header("Respawn Stats")]
    [SerializeField] private RespawnData _respawnData;

    [Header("Hunt Stats")]
    [SerializeField] private HuntData _huntData;

    [Header("Gather Stats")]
    [SerializeField] private GatherData _gatherData;

    [Header("Leave Stats")]
    [SerializeField] private LeaveData _leaveData;

    protected override void Awake()
    {
        base.Awake();

        StateScout stateScout = new StateScout(this, _hunterUI, _scoutData, _stateMachine);
        StateRetreat stateRetreat = new StateRetreat(this, _hunterUI, _retreatData, _stateMachine);
        StateRest stateRest = new StateRest(this, _hunterUI, _restData, _stateMachine);
        StateRespawn stateRespawn = new StateRespawn(this, _hunterUI, _respawnData, _stateMachine);
        StateHunt stateHunt = new StateHunt(this, _hunterUI, _huntData, _stateMachine);
        StateGather stateGather = new StateGather(this, _hunterUI, _gatherData, _stateMachine);
        StateLeave stateLeave = new StateLeave(this, _hunterUI, _leaveData, _stateMachine);

        _stateMachine.RegisterState(HunterMode.Scout, stateScout);
        _stateMachine.RegisterState(HunterMode.Retreat, stateRetreat);
        _stateMachine.RegisterState(HunterMode.Rest, stateRest);
        _stateMachine.RegisterState(HunterMode.Respawn, stateRespawn);
        _stateMachine.RegisterState(HunterMode.Hunt, stateHunt);
        _stateMachine.RegisterState(HunterMode.Gather, stateGather);
        _stateMachine.RegisterState(HunterMode.Leave, stateLeave);

        _stateMachine.StartFirstState(HunterMode.Scout);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }
}
