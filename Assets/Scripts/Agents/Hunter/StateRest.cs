using UnityEngine;

public class StateRest : State
{
    public StateRest(Wolf agent, HunterUI UI, Animator anim, RestData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private Wolf _agent;
    private HunterUI _UI;
    private Animator _anim;
    private RestData _data;

    private float _timer;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Rest);
        _anim.SetTrigger("IsIdle");

        _timer = _data._restTime;
        _UI.UpdateBar(_timer, _data._restTime, HunterMode.Rest);
        _agent.StopVelocity();
    }

    public override void Update()
    {
        if (_timer > 0)
        {
            _timer += Time.deltaTime;
            _UI.UpdateBar(_timer, _data._restTime, HunterMode.Rest);
        }

        if (_timer <= 0)
        {
            _stateMachine.ChangeState(HunterMode.Scout);
        }
    }

    public override void Exit()
    {
        _timer = _data._restTime;
    }
}

[System.Serializable]
public class RestData
{
    public float _restTime;
}