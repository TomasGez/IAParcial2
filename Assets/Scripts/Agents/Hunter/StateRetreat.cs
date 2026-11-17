using UnityEngine;

public class StateRetreat : State
{
    public StateRetreat(Wolf agent, HunterUI UI, Animator anim, FSM stateMachine) : base(stateMachine)
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
        _UI.ChangeMode(HunterMode.Retreat);
        _anim.SetTrigger("IsWalking");

        _agent.FastSpeed(1f);

        _agent.SeekClosestNode(Zone.Safe);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        _agent.FastSpeed();
        _agent.StopWalkPath();

        _anim.ResetTrigger("IsWalking");
    }
}
