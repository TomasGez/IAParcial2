using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : State
{
    public StatePatrol(Ghost agent, KillerTrigger trigger, KillerUI UI, PatrolData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _trigger = trigger;
        _UI = UI;
        _data = data;
    }

    private Ghost _agent;
    private KillerTrigger _trigger;
    private KillerUI _UI;
    private PatrolData _data;

    public override void Enter()
    {
        if (_agent.WolfTarget != null)
        {
            _stateMachine.ChangeState(KillerMode.Track);
            return;
        }

        _UI.ChangeMode(KillerMode.Patrol);

        _agent.SeekLoop(_data.patrolNodes);
    }

    public override void Update()
    {
        if (_trigger.CanSeeTarget && _agent.WolfTarget != null)
        {
            _agent.WolfTarget.IsBeingFollowed();
            _stateMachine.ChangeState(KillerMode.Chase);
            return;
        }

        if (!_trigger.CanSeeTarget && _agent.WolfTarget != null)
        {
            _stateMachine.ChangeState(KillerMode.Track);
            return;
        }
    }

    public override void Exit()
    {
        _agent.StopWalkPath();
    }
}

[System.Serializable]
public class PatrolData
{
    public List<Node> patrolNodes;
}
