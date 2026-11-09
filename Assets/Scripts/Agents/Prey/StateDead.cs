using UnityEngine;

public class StateDead : State
{
    public StateDead(PreyUI UI, Animator Anim, FSM stateMachine) : base(stateMachine)
    {
        _UI = UI;
        _anim = Anim;
    }

    private PreyUI _UI;
    private Animator _anim;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Dead);
        _anim.SetBool("IsDead", true);
    }

    public override void Update() { }

    public override void Exit() { }
}
