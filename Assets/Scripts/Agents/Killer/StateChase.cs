using UnityEngine;

public class StateChase : State
{
    public StateChase(Ghost agent, KillerTrigger trigger, KillerUI UI, FSM stateMachine) : base(stateMachine)
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
        _UI.ChangeMode(KillerMode.Chase);

        _agent.FastSpeed(0.75f);
    }

    public override void Update()
    {
        if (_agent.WolfTarget.IsSafe || _agent.WolfTarget.IsDead)
        {
            _agent.TargetExited(_agent.WolfTarget);
            _stateMachine.ChangeState(KillerMode.Patrol);
            return;
        }

        if (!_trigger.CanSeeTarget && _agent.WolfTarget != null && !_agent.WolfTarget.IsDead)
        {
            _stateMachine.ChangeState(KillerMode.Track);
            return;
        }

        _agent.Seek(_agent.WolfTarget.transform.position);
    }

    public override void Exit()
    {
        _agent.FastSpeed();
    }
}
