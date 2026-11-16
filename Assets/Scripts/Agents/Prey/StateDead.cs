using UnityEngine;

public class StateDead : State
{
    public StateDead(AgentPrey agent, PreyUI UI, Animator anim, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
    }

    private AgentPrey _agent;
    private PreyUI _UI;
    private Animator _anim;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Dead);
        _anim.SetTrigger("IsDead");

        _agent.StopVelocity();
    }

    public override void Update() { }

    public override void Exit() { }
}
