using UnityEngine;

public class StateRespawn : State
{
    public StateRespawn(AgentHunter agent, HunterUI UI, RespawnData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private RespawnData _data;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class RespawnData
{
    
}
