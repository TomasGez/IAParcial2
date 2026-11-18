using UnityEngine;

public class StateTrack : State
{
    public StateTrack(Ghost agent, KillerTrigger trigger, KillerUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _trigger = trigger;
        _UI = UI;
    }

    private Ghost _agent;
    private KillerTrigger _trigger;
    private KillerUI _UI;

    public override void Enter()
    {
        _UI.ChangeMode(KillerMode.Track);

        _agent.SeekNextNode();
    }

    public override void Update()
    {
        if (_agent.WolfTarget.IsSafe || _agent.WolfTarget.IsDead)
        {
            _agent.TargetExited(_agent.WolfTarget);
            _stateMachine.ChangeState(KillerMode.Patrol);
            return;
        }

        if (_trigger.CanSeeTarget && _agent.WolfTarget != null)
        {
            _agent.WolfTarget.IsBeingFollowed();
            _stateMachine.ChangeState(KillerMode.Chase);
            return;
        }
    }

    public override void Exit()
    {
        _agent.StopWalkPath();
    }
}
