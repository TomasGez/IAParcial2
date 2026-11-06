using UnityEngine;

public class StateLeave : State
{
    public StateLeave(AgentHunter agent, HunterUI UI, LeaveData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private LeaveData _data;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class LeaveData
{
    
}
