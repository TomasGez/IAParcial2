using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    protected FSM _stateMachine;
    private Coroutine _keepLookingRoutine;
    
    private List<Agent> _targets = new List<Agent>();
    public Agent CurrentTarget { get; protected set; }

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

    protected void TargetEntered(Agent target)
    {
        if (target == CurrentTarget && _keepLookingRoutine != null)
        {
            StopCoroutine(_keepLookingRoutine);
            _keepLookingRoutine = null;
        }

        if (!_targets.Contains(target)) _targets.Add(target);

        if (CurrentTarget == null) CurrentTarget = _targets[0];
    }

    protected void TargetExited(Agent target)
    {
        if (target != CurrentTarget)
        {
            if (_targets.Contains(target)) _targets.Remove(target);
        }
        else if (_keepTargetTime > 0)
        {
            if (_keepLookingRoutine == null)
                _keepLookingRoutine = StartCoroutine(KeepLooking(target));
        }
        else
        {
            if (_targets.Count > 0) CurrentTarget = _targets[0];

            else CurrentTarget = null;
        }
    }

    public void ClearTargets()
    {
        if (_keepLookingRoutine != null)
        {
            StopCoroutine(_keepLookingRoutine);
            _keepLookingRoutine = null;
        }

        _targets.Clear();

        CurrentTarget = null;
    }

    protected IEnumerator KeepLooking(Agent target)
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
            }
        }
    }

    protected Vector3 CalculatedDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction *= _currentSpeed;

        return direction;
    }

    protected Vector3 CalculatedSteering(Vector3 desiredDirection)
    {
        Vector3 steering =  desiredDirection - _currentVelocity;

        steering = Vector3.ClampMagnitude(steering, _maxSteering * Time.deltaTime);

        return steering;
    }

    protected Vector3 CalculatedFuture(Agent target)
    {
        Vector3 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;

        float prediction = distance / (_maxSpeed + target._currentVelocity.magnitude);
        Vector3 futurePosition = target.transform.position + target._currentVelocity * prediction;
        return futurePosition;
    }
}
