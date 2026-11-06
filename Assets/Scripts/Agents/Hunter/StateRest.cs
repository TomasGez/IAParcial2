using UnityEngine;

public class StateRest : State
{
    public StateRest(AgentHunter agent, HunterUI UI, RestData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private RestData _data;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class RestData
{
    
}