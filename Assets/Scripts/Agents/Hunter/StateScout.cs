using UnityEngine;

public class StateScout : State
{
    public StateScout(AgentHunter agent, HunterUI UI, ScoutData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private ScoutData _data;

    public override void Enter()
    {
        _UI.UpdateCollected(1, 3);
        _UI.ChangeMode(HunterMode.Scout);
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class ScoutData
{
    
}
