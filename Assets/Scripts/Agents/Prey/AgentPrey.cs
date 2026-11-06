using System.Collections.Generic;
using UnityEngine;

public class AgentPrey : AgentBoid
{
    [SerializeField] private PreyUI _preyUI;
    private bool isBeingFollowed;
    private bool wasKilled;

    [Header("Flock Stats")]
    [SerializeField] private FlockData _flockData;

    [Header("Escape Stats")]
    [SerializeField] private EscapeData _escapeData;

    [Header("Dead Stats")]
    [SerializeField] private DeadData _deadData;

    protected override void Awake()
    {
        base.Awake();

        StateFlock stateFlock = new StateFlock(this, _preyUI, _flockData, _stateMachine);
        StateEscape stateEscape = new StateEscape(this, _preyUI, _escapeData, _stateMachine);
        StateDead stateDead = new StateDead(this, _preyUI, _deadData, _stateMachine);

        _stateMachine.RegisterState(PreyMode.Flock, stateFlock);
        _stateMachine.RegisterState(PreyMode.Escape, stateEscape);
        _stateMachine.RegisterState(PreyMode.Dead, stateDead);

        _stateMachine.StartFirstState(PreyMode.Flock);

        _allPreys.Add(this);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }

    public void IsBeingFollowed(bool isActive)
    {
        isBeingFollowed = isActive;

        if (isBeingFollowed && !wasKilled)
        {
            _stateMachine.ChangeState(PreyMode.Escape);
        }
    }

    public void WasKilled(bool isActive)
    {
        wasKilled = isActive;

        if (wasKilled)
        {
            _allPreys.Remove(this);
            _stateMachine.ChangeState(PreyMode.Dead);
        }
    }

    public void TerminatePrey()
    {
        Destroy(gameObject);
    }
}
