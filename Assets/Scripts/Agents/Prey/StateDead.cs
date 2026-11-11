using UnityEngine;

public class StateDead : State
{
    public StateDead(PreyUI UI, Animator anim, FSM stateMachine) : base(stateMachine)
    {
        _UI = UI;
        _anim = anim;
    }

    private PreyUI _UI;
    private Animator _anim;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Dead);
        _anim.SetTrigger("IsDead");
    }

    public override void Update() { }

    public override void Exit() { }
}
