using UnityEngine;

public class StateTrack : State
{
    public StateTrack(Ghost agent, KillerUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private Ghost _agent;
    private KillerUI _UI;

    public override void Enter()
    {
        _UI.ChangeMode(KillerMode.Track);
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}
