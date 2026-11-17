using UnityEngine;

public class Sheep : AgentPrey
{
    private PreyTrigger _preyTrigger;
    private PreyUI _preyUI;
    private Animator _preyAnim;

    private StateFlock _stateFlock;
    private StateEscape _stateEscape;
    private StateDead _stateDead;

    public bool IsDead { get; private set; }

    [Header("Flock Stats")]
    [SerializeField] private FlockData _flockData;

    protected override void Awake()
    {
        base.Awake();

        _preyTrigger = GetComponentInChildren<PreyTrigger>();
        _preyUI = GetComponentInChildren<PreyUI>();
        _preyAnim = GetComponentInChildren<Animator>();

        _stateFlock = new StateFlock(this, _preyUI, _flockData, _stateMachine);
        _stateEscape = new StateEscape(this, _preyUI, _stateMachine);
        _stateDead = new StateDead(this, _preyUI, _preyAnim, _stateMachine);

        _stateMachine.RegisterState(PreyMode.Flock, _stateFlock);
        _stateMachine.RegisterState(PreyMode.Escape, _stateEscape);
        _stateMachine.RegisterState(PreyMode.Dead, _stateDead);

        _allPreys.Add(this);

        _stateMachine.StartFirstState(PreyMode.Flock);
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
        Manager.Instance.RemoveSheep();
        Destroy(gameObject);
    }
}
