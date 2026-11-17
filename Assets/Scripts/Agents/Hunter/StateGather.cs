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

    private float _timer;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Gather);
        _anim.SetTrigger("IsIdle");

        _timer = _data._gatherTime;
        _UI.UpdateBar(_timer, _data._gatherTime, HunterMode.Gather);
        _agent.StopVelocity();
    }

    public override void Update()
    {
        if (_agent.SheepTarget == null)
        {
            _stateMachine.ChangeState(HunterMode.Scout);
            return;
        }

        _agent.Pursuit(_agent.SheepTarget, _data._corpseSlowingDistance, _data._corpseMinDistance);

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            _UI.UpdateBar(_timer, _data._gatherTime, HunterMode.Gather);
        }

        if (_timer <= 0)
        {
            _agent.SheepTarget.TerminatePrey();
            _agent.TargetExited(_agent.SheepTarget);
            _agent.PreyCollected();
            _stateMachine.ChangeState(HunterMode.Scout);
        }
    }

    public override void Exit()
    {
        _agent.FastSpeed();
        _timer = _data._gatherTime;

        _UI.UpdateCollected(_agent.CollectedPreys, _agent.GoalCollection);
        _anim.ResetTrigger("IsIdle");
    }
}

[System.Serializable]
public class GatherData
{
    public float _corpseSlowingDistance;
    public float _corpseMinDistance;
    public float _gatherTime;
}
