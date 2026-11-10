using UnityEngine;

public class StateScout : State
{
    public StateScout(AgentHunter agent, HunterUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private AgentHunter _agent;
    private HunterUI _UI;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Scout);

        _agent.SeekRandom();
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        _agent.StopWalkPath();
    }
}
