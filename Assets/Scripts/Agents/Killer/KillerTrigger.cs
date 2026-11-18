using System;
using UnityEngine;

public class KillerTrigger : FieldOfViewSensor
{
    public event Action<Wolf> OnHunterEnter;
    private bool _targetInRange = false;

    private void Awake()
    {
        _agent = GetComponentInParent<Ghost>();
    }

    private void Update()
    {
        _canSeeTarget = _targetInRange && _target != null && InViewAngle() && InLineOfSight();
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            if (other.TryGetComponent<Wolf>(out var wolf))
            {
                _target = wolf;
                _targetInRange = true;

                if (_targetInRange && _target != null && InViewAngle() && InLineOfSight()) OnHunterEnter?.Invoke(wolf);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            if (other.TryGetComponent<Wolf>(out var wolf))
            {
                _target = null;
                _targetInRange = false;
            }
        }
    }
}
