using UnityEngine;

public class StateGather : State
{
    public StateGather(AgentHunter agent, HunterUI UI, GatherData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private GatherData _data;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class GatherData
{
    
}
