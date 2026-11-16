using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            if (other.TryGetComponent<Wolf>(out var wolf))
            {
                _targetInRange = true;
                OnHunterEnter?.Invoke(wolf);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            if (other.TryGetComponent<Wolf>(out var wolf)) _targetInRange = false;
        }
    } 
}
