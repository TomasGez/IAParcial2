using UnityEngine;

public class StateScout : State
{
    public StateScout(Wolf agent, HunterUI UI, Animator anim, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
    }

    private Wolf _agent;
    private HunterUI _UI;
    private Animator _anim;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Scout);
        _anim.SetTrigger("IsWalking");

        _agent.SeekRandom();

        if (_agent.CollectedPreys >= _agent.GoalCollection) _stateMachine.ChangeState(HunterMode.Leave);
    }

    public override void Update()
    {
        if (_agent.SheepTarget != null) _stateMachine.ChangeState(HunterMode.Hunt);
    }

    public override void Exit()
    {
        _agent.StopWalkPath();
    }
}
