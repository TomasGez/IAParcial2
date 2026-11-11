using UnityEngine;

public class StateEscape : State
{
    public StateEscape(AgentPrey agent, PreyUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private AgentPrey _agent;
    private PreyUI _UI;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Escape);
    }

    public override void Update()
    {
        if (_agent.CurrentTarget == null) _stateMachine.ChangeState(PreyMode.Flock);

        if (_agent.CurrentTarget != null) _agent.Evade(_agent.CurrentTarget);
    }

    public override void Exit() { }
}
