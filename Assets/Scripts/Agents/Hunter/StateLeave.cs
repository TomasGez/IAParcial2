using UnityEngine;

public class StateLeave : State
{
    public StateLeave(AgentHunter agent, HunterUI UI, Animator anim, LeaveData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private Animator _anim;
    private LeaveData _data;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Leave);
        _anim.SetTrigger("IsWalking");
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class LeaveData
{
    
}
