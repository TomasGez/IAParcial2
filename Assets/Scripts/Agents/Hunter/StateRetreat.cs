using UnityEngine;

public class StateRetreat : State
{
    public StateRetreat(AgentHunter agent, HunterUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private AgentHunter _agent;
    private HunterUI _UI;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}
