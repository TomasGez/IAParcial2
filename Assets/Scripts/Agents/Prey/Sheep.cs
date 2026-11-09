using UnityEngine;

public class Sheep : AgentPrey
{
    private PreyTrigger _preyTrigger;
    private PreyUI _preyUI;
    public bool IsDead { get; private set; }

    [Header("Flock Stats")]
    [SerializeField] private FlockData _flockData;

    [Header("Dead Stats")]
    [SerializeField] private DeadData _deadData;

    protected override void Awake()
    {
        base.Awake();

        _preyTrigger = GetComponentInChildren<PreyTrigger>();
        _preyUI = GetComponentInChildren<PreyUI>();

        StateFlock stateFlock = new StateFlock(this, _preyUI, _flockData, _stateMachine);
        StateEscape stateEscape = new StateEscape(this, _preyUI, _stateMachine);
        StateDead stateDead = new StateDead(this, _preyUI, _deadData, _stateMachine);

        _stateMachine.RegisterState(PreyMode.Flock, stateFlock);
        _stateMachine.RegisterState(PreyMode.Escape, stateEscape);
        _stateMachine.RegisterState(PreyMode.Dead, stateDead);

        _stateMachine.StartFirstState(PreyMode.Flock);

        _allPreys.Add(this);
    }

    private void OnEnable()
    {
        if (_preyTrigger == null) return;

        _preyTrigger.OnHunterEnter += TargetEntered;
        _preyTrigger.OnHunterExit += TargetExited;
    }

    private void OnDisable()
    {
        if (_preyTrigger == null) return;

        _preyTrigger.OnHunterEnter -= TargetEntered;
        _preyTrigger.OnHunterExit -= TargetExited;
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }

    public void WasKilled(bool isActive)
    {
        IsDead = isActive;

        if (IsDead)
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
