using UnityEngine;

public class StateRespawn : State
{
    public StateRespawn(AgentHunter agent, HunterUI UI, Animator anim, RespawnData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private AgentHunter _agent;
    private HunterUI _UI;
    private Animator _anim;
    private RespawnData _data;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Respawn);
        _anim.SetTrigger("IsDead");
    }

    public override void Update()
    {
        
    }

    public override void Exit() { }
}

[System.Serializable]
public class RespawnData
{
    
}
