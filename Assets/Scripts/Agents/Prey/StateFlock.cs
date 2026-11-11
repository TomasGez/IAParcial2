using UnityEngine;

public class StateFlock : State
{
    public StateFlock(AgentPrey agent, PreyUI UI, FlockData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentPrey _agent;
    private PreyUI _UI;
    private FlockData _data;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Flock);

        _agent.RandomDirection();
    }

    public override void Update()
    {
        if (_agent.CurrentTarget != null) _stateMachine.ChangeState(PreyMode.Escape);

        _agent.Flocking(_data.separationRadius, _data.separationWeight,
                        _data.alignmentRadius, _data.alignmentWeight,
                        _data.cohesionRadius, _data.cohesionWeight);
    }

    public override void Exit() { }
}

[System.Serializable]
public class FlockData
{
    public float separationRadius;
    public float alignmentRadius;
    public float cohesionRadius;
    [Range(0f, 1f)] public float separationWeight;
    [Range(0f, 1f)] public float alignmentWeight;
    [Range(0f, 1f)] public float cohesionWeight;
}
