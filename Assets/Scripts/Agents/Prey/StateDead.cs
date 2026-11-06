using UnityEngine;

public class StateDead : State
{
    public StateDead(AgentPrey agent, PreyUI UI, DeadData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentPrey _agent;
    private PreyUI _UI;
    private DeadData _data;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Dead);
    }

    public override void Update() { }

    public override void Exit() { }
}

[System.Serializable]
public class DeadData
{
    
}
