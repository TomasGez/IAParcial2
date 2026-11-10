using UnityEngine;

public class StateTrack : State
{
    public StateTrack(AgentKiller agent, KillerUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private AgentKiller _agent;
    private KillerUI _UI;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}
