using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    protected FSM _stateMachine;
    private Coroutine _keepLookingCoroutine;
    
    private List<AgentNPC> _targets = new List<AgentNPC>();
    public AgentNPC CurrentTarget { get; protected set; }

    [Header("Agent Stats")]
    protected Vector3 _currentVelocity;
    [SerializeField] protected float _maxSpeed;
    protected float _currentSpeed;
    [SerializeField] protected float _maxSteering;

    [Header("Sensor")]
    [SerializeField] private float _viewAngle = 90f;
    [SerializeField] private float _eyeHeight = 1f;
    [SerializeField] private float _keepTargetTime;

    [Header("Layer")]
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;

    public float ViewAngle => _viewAngle;
    public float EyeHeight => _eyeHeight;
    public LayerMask TargetMask => _targetMask;
    public LayerMask ObstacleMask => _obstacleMask;

    protected virtual void Awake()
    {
        _stateMachine = new FSM();

        _currentSpeed = _maxSpeed;
    }

    protected virtual void Movement()
    {
        transform.position += _currentVelocity * Time.deltaTime;

        if (_currentVelocity != Vector3.zero)
        {
            transform.forward = _currentVelocity;
        }
    }

    protected void TargetEntered(AgentNPC target, Enum key)
    {
        if (target == CurrentTarget && _keepLookingCoroutine != null)
        {
            StopCoroutine(_keepLookingCoroutine);
            _keepLookingCoroutine = null;
        }

        if (!_targets.Contains(target)) _targets.Add(target);

        if (CurrentTarget == null)
        {
            CurrentTarget = _targets[0];

            if (CurrentTarget != null) _stateMachine.ChangeState(key);
        }
    }

    protected void TargetExited(AgentNPC target, Enum key)
    {
        if (target != CurrentTarget)
        {
            if (_targets.Contains(target)) _targets.Remove(target);
        }
        else
        {
            if (_keepLookingCoroutine == null)
                _keepLookingCoroutine = StartCoroutine(KeepLooking(target, key));
        }
    }

    public void ClearTargets(Enum key)
    {
        if (_keepLookingCoroutine != null)
        {
            StopCoroutine(_keepLookingCoroutine);
            _keepLookingCoroutine = null;
        }

        _targets.Clear();

        CurrentTarget = null;

        _stateMachine.ChangeState(key);
    }

    protected IEnumerator KeepLooking(AgentNPC target, Enum key)
    {
        yield return new WaitForSeconds(_keepTargetTime);

        if (_targets.Contains(target)) _targets.Remove(target);

        if (target == CurrentTarget)
        {
            if (_targets.Count > 0)
            {
                CurrentTarget = _targets[0];
            }
            else
            {
                CurrentTarget = null;

                _stateMachine.ChangeState(key);
            }
        }
    }
}
