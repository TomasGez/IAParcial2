using UnityEngine;

public class StateEscape : State
{
    public StateEscape(Sheep agent, PreyUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private Sheep _agent;
    private PreyUI _UI;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Escape);

        _agent.FastSpeed(0.5f);
    }

    public override void Update()
    {
        if (_agent.CurrentTarget == null) _stateMachine.ChangeState(PreyMode.Flock);

        else _agent.Evade(_agent.CurrentTarget);
    }

    public override void Exit()
    {
        _agent.FastSpeed();
    }
}
