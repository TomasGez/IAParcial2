using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : State
{
    public StatePatrol(AgentKiller agent, KillerUI UI, PatrolData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _data = data;
    }

    private AgentKiller _agent;
    private KillerUI _UI;
    private PatrolData _data;

    public override void Enter()
    {
        _UI.ChangeMode(KillerMode.Patrol);

        _agent.SeekLoop(_data.patrolNodes);
        
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        _agent.StopWalkPath();
        Debug.Log("salio");
    }
}

[System.Serializable]
public class PatrolData
{
    public List<Node> patrolNodes;
}
