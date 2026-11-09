using System;
using UnityEngine;

public class KillerTrigger : FieldOfViewSensor
{
    public event Action<Agent> OnHunterEnter;
    private bool _targetInRange = false;

    private void Awake()
    {
        _agent = GetComponentInParent<Ghost>();
    }

    private void Update()
    {
        _canSeeTarget = _targetInRange && _target != null && InViewAngle() && InLineOfSight();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            Wolf target = other.GetComponentInParent<Wolf>();
            _target = target;
            
            if (target == null) return;

            OnHunterEnter?.Invoke(target);
            _targetInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            Wolf target = other.GetComponentInParent<Wolf>();

            if (target == null) return;

            if (target == _target) _targetInRange = false;
        }
    }

    
}
