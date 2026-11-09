using UnityEngine;

public class Sheep : AgentPrey
{
    private PreyTrigger _preyTrigger;
    private PreyUI _preyUI;
    private Animator _preyAnim;

    public bool IsDead { get; private set; }

    [Header("Flock Stats")]
    [SerializeField] private FlockData _flockData;

    protected override void Awake()
    {
        base.Awake();

        _preyTrigger = GetComponentInChildren<PreyTrigger>();
        _preyUI = GetComponentInChildren<PreyUI>();
        _preyAnim = GetComponentInChildren<Animator>();

        StateFlock stateFlock = new StateFlock(this, _preyUI, _flockData, _stateMachine);
        StateEscape stateEscape = new StateEscape(this, _preyUI, _stateMachine);
        StateDead stateDead = new StateDead(_preyUI, _preyAnim, _stateMachine);

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
        if (!IsDead)
        {
            if (CurrentTarget != null) _stateMachine.ChangeState(PreyMode.Escape);

            else _stateMachine.ChangeState(PreyMode.Flock);
        }

        _stateMachine.Update();

        Movement();

        if (Input.GetKeyDown(KeyCode.Space)) WasKilled(true);
    }

    public void WasKilled(bool isActive)
    {
        IsDead = isActive;

        if (IsDead)
        {
            _allPreys.Remove(this);
            _stateMachine.ChangeState(PreyMode.Dead);
            _currentVelocity = Vector3.zero;
        }
    }

    public void TerminatePrey()
    {
        _allPreys.Remove(this);
        Destroy(gameObject);
    }
}
