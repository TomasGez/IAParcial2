using UnityEngine;

public class StateRest : State
{
    public StateRest(AgentHunter agent, HunterUI UI, Animator anim, RestData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private Animator _anim;
    private RestData _data;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Rest);
        _anim.SetTrigger("IsIdle");
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class RestData
{
    
}