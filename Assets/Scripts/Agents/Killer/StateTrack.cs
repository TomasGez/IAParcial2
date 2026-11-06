using UnityEngine;

public class StateTrack : State
{
    public StateTrack(AgentKiller agent, KillerUI UI, TrackData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentKiller _agent;
    private KillerUI _UI;
    private TrackData _data;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class TrackData
{
    
}
