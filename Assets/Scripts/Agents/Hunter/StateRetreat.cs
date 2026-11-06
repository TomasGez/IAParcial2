using UnityEngine;

public class StateRetreat : State
{
    public StateRetreat(AgentHunter agent, HunterUI UI, RetreatData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private RetreatData _data;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class RetreatData
{
    
}
