using UnityEngine;

public class StateEscape : State
{
    public StateEscape(AgentPrey agent, PreyUI UI, EscapeData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentPrey _agent;
    private PreyUI _UI;
    private EscapeData _data;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Escape);
    }

    public override void Update()
    {
        if (_agent.GetTargets() != null && _agent.GetTargets().Count > 0 && _agent.CurrentTarget != null)
        {
            _agent.Evade(_agent.CurrentTarget);
        }
        else
        {
            _agent.IsBeingFollowed(false);
            _stateMachine.ChangeState(PreyMode.Flock);
        }
    }

    public override void Exit() { }
}

[System.Serializable]
public class EscapeData
{
    
}
