using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    protected FSM _stateMachine;
    protected Vector3 _currentVelocity;
    private List<Agent> _targets = new List<Agent>();
    private Agent _currentTarget;

    [Header("Agent Stats")]
    [SerializeField] protected float maxSpeed;
    protected float _currentSpeed;
    [SerializeField] protected float maxSteering;

    protected virtual void Awake()
    {
        _stateMachine = new FSM();

        _currentSpeed = maxSpeed;
    }

    protected virtual void Movement()
    {
        transform.position += _currentVelocity * Time.deltaTime;

        if (_currentVelocity != Vector3.zero)
        {
            transform.forward = _currentVelocity;
        }
    }

    public List<Agent> GetTargets()
    {
        return _targets;
    }

    public void AddTarget(Agent agent)
    {
        _targets.Add(agent);

        if (_targets.Count == 0) _currentTarget = agent;
    }

    public void RemoveTarget(Agent agent)
    {
        _targets.Remove(agent);

        if (agent == _currentTarget && _targets.Count > 0) _currentTarget = _targets[0];

        else if (_targets.Count == 0) _currentTarget = null;
    }

    public void ClearTargets()
    {
        _targets.Clear();

        _currentTarget = null;
    }

    public Agent GetCurrentTarget()
    {
        return _currentTarget;
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

        steering = Vector3.ClampMagnitude(steering, maxSteering * Time.deltaTime);

        return steering;
    }

    protected Vector3 CalculatedFuture(Agent target)
    {
        Vector3 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;

        float prediction = distance / (maxSpeed + target._currentVelocity.magnitude);
        Vector3 futurePosition = target.transform.position + target._currentVelocity * prediction;
        return futurePosition;
    }
}
