using UnityEngine;

public class StateHunt : State
{
    public StateHunt(AgentHunter agent, HunterUI UI, Animator anim, HuntData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private Animator _anim;
    private HuntData _data;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Hunt);
        _anim.SetTrigger("IsWalking");
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class HuntData
{
    
}
