using UnityEngine;

public class StateRespawn : State
{
    public StateRespawn(Wolf agent, HunterUI UI, Animator anim, RespawnData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private Wolf _agent;
    private HunterUI _UI;
    private Animator _anim;
    private RespawnData _data;

    private float _timer = 0f;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Respawn);

        if (_agent.IsDead)
        {
            _anim.SetTrigger("IsDead");
        }

        else
        {
            _anim.SetTrigger("IsCelebrating");
        }
        
        _timer = 0f;
        _UI.UpdateBar(_timer, _data._respawnTime, HunterMode.Respawn);
        _agent.StopVelocity();
    }

    public override void Update()
    {
        if (_timer < _data._respawnTime)
        {
            _timer += Time.deltaTime;
            _UI.UpdateBar(_timer, _data._respawnTime, HunterMode.Respawn);
        }

        if (_timer >= _data._respawnTime)
        {
            if (_agent.IsDead)
            {
                _agent.WasKilled(false);
            }

            else
            {
                _agent.ResetCollection();
            }

            _agent.RestartFromHome();
            _stateMachine.ChangeState(HunterMode.Scout);
        }
    }

    public override void Exit()
    {
        _agent.FastSpeed();
        _timer = 0f;

        _UI.UpdateCollected(_agent.CollectedPreys, _agent.GoalCollection);
        _anim.ResetTrigger("IsDead");
        _anim.ResetTrigger("IsCelebrating");
    }
}

[System.Serializable]
public class RespawnData
{
    public float _respawnTime;
}
