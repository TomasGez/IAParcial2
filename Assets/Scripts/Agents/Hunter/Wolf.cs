using UnityEngine;

public class Wolf : AgentHunter
{
    private HunterTrigger _hunterTrigger;
    private HunterUI _hunterUI;
    private Animator _hunterAnim;

    private StateScout _stateScout;
    private StateRetreat _stateRetreat;
    private StateRest _stateRest;
    private StateRespawn _stateRespawn;
    private StateHunt _stateHunt;
    private StateGather _stateGather;
    private StateLeave _stateLeave;

    public bool IsDead { get; private set; }
    public bool IsBeingFollowed { get; private set; }
    public bool IsSafe { get; private set; }

    [Header("Wolf Stats")]
    [SerializeField] private int _goalCollection;
    public int GoalCollection => _goalCollection;
    [SerializeField] private RestData _restData;
    [SerializeField] private RespawnData _respawnData;
    [SerializeField] private HuntData _huntData;
    [SerializeField] private GatherData _gatherData;

    protected override void Awake()
    {
        base.Awake();

        _hunterTrigger = GetComponentInChildren<HunterTrigger>();
        _hunterUI = GetComponentInChildren<HunterUI>();
        _hunterAnim = GetComponentInChildren<Animator>();

        _stateScout = new StateScout(this, _hunterUI, _hunterAnim, _stateMachine);
        _stateRetreat = new StateRetreat(this, _hunterUI, _hunterAnim, _stateMachine);
        _stateRest = new StateRest(this, _hunterUI, _hunterAnim, _restData, _stateMachine);
        _stateRespawn = new StateRespawn(this, _hunterUI, _hunterAnim, _respawnData, _stateMachine);
        _stateHunt = new StateHunt(this, _hunterUI, _hunterAnim, _huntData, _stateMachine);
        _stateGather = new StateGather(this, _hunterUI, _hunterAnim, _gatherData, _stateMachine);
        _stateLeave = new StateLeave(this, _hunterUI, _hunterAnim, _stateMachine);

        _stateMachine.RegisterState(HunterMode.Scout, _stateScout);
        _stateMachine.RegisterState(HunterMode.Retreat, _stateRetreat);
        _stateMachine.RegisterState(HunterMode.Rest, _stateRest);
        _stateMachine.RegisterState(HunterMode.Respawn, _stateRespawn);
        _stateMachine.RegisterState(HunterMode.Hunt, _stateHunt);
        _stateMachine.RegisterState(HunterMode.Gather, _stateGather);
        _stateMachine.RegisterState(HunterMode.Leave, _stateLeave);

        _hunterUI.UpdateCollected(CollectedPreys, GoalCollection);

        _stateMachine.StartFirstState(HunterMode.Scout);
    }

    private void OnEnable()
    {
        if (_hunterTrigger == null) return;

        _hunterTrigger.OnPreyEnter += TargetEntered;
        _hunterTrigger.OnPreyExit += TargetExited;
    }

    private void OnDisable()
    {
        if (_hunterTrigger == null) return;

        _hunterTrigger.OnPreyEnter -= TargetEntered;
        _hunterTrigger.OnPreyExit -= TargetExited;
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
            _stateMachine.ChangeState(HunterMode.Respawn);
        }
    }

    public void HasSomeoneChasing(bool isActive)
    {
        IsBeingFollowed = isActive;

        if (IsBeingFollowed)
        {
            _stateMachine.ChangeState(HunterMode.Retreat);
        }
    }
}
