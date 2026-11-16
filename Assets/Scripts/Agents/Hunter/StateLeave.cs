using UnityEngine;

public class StateLeave : State
{
    public StateLeave(AgentHunter agent, HunterUI UI, Animator anim, FSM stateMachine) : base(stateMachine)
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
        _UI.ChangeMode(HunterMode.Leave);
        _anim.SetTrigger("IsWalking");

        _agent.SeekClosestNode(Zone.Goal);
    }

    public override void Update()
    {
        
    }

    public override void Exit() 
    {
        _agent.StopWalkPath();
    }
}
