using UnityEngine;

public class StateRetreat : State
{
    public StateRetreat(AgentHunter agent, HunterUI UI, Animator anim, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private Animator _anim;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Retreat);
        _anim.SetTrigger("IsWalking");
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}
