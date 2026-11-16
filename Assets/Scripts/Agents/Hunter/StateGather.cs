using UnityEngine;

public class StateGather : State
{
    public StateGather(Wolf agent, HunterUI UI, Animator anim, GatherData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private Wolf _agent;
    private HunterUI _UI;
    private Animator _anim;
    private GatherData _data;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Gather);
        _anim.SetTrigger("IsIdle");

        _agent.StopVelocity();
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class GatherData
{
    
}
