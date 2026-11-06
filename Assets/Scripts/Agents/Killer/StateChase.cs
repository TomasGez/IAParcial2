using UnityEngine;

public class StateChase : State
{
    public StateChase(AgentKiller agent, KillerUI UI, ChaseData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentKiller _agent;
    private KillerUI _UI;
    private ChaseData _data;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class ChaseData
{
    
}
