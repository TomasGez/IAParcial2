using UnityEngine;

public class StateChase : State
{
    public StateChase(Ghost agent, KillerUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private Ghost _agent;
    private KillerUI _UI;

    public override void Enter()
    {
        _UI.ChangeMode(KillerMode.Chase);

        _agent.FastSpeed(1f);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        _agent.FastSpeed();
    }
}
