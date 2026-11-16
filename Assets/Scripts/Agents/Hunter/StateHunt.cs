using UnityEngine;

public class StateHunt : State
{
    public StateHunt(Wolf agent, HunterUI UI, Animator anim, HuntData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private Wolf _agent;
    private HunterUI _UI;
    private Animator _anim;
    private HuntData _data;

    private float _timer;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Hunt);
        _anim.SetTrigger("IsWalking");

        _agent.FastSpeed(0.5f);

        if (_agent.CollectedPreys >= _agent.GoalCollection) _stateMachine.ChangeState(HunterMode.Leave);
    }

    public override void Update()
    {
        if (_agent.SheepTarget == null)
        {
            _stateMachine.ChangeState(HunterMode.Scout);
            return;
        }

        if (_agent.SheepTarget != null)
        {
            if (_agent.SheepTarget.IsDead) 
            {
                _stateMachine.ChangeState(HunterMode.Gather);
                return;
            }

            else _agent.Pursuit(_agent.SheepTarget, _data._preySlowingDistance, _data._preyMinDistance);
        }

        if (_timer < _data._huntTime)
        {
            _timer += Time.deltaTime;
            _UI.UpdateBar(_timer, _data._huntTime, HunterMode.Hunt);
        }

        if (_timer >= _data._huntTime)
        {
            _agent.SheepTarget.WasKilled(true);
            _timer = 0;
        }
    }

    public override void Exit()
    {
        _agent.FastSpeed();
    }
}

[System.Serializable]
public class HuntData
{
    public float _preySlowingDistance;
    public float _preyMinDistance;
    public float _huntTime;
}
