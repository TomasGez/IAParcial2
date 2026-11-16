using System;
using UnityEngine;

public class PreyTrigger : FieldOfViewSensor
{
    public event Action<Wolf> OnHunterEnter;
    public event Action<Wolf> OnHunterExit;

    private void Awake()
    {
        _agent = GetComponentInParent<Sheep>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            if (other.TryGetComponent<Wolf>(out var wolf)) OnHunterEnter?.Invoke(wolf);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            if (other.TryGetComponent<Wolf>(out var wolf)) OnHunterExit?.Invoke(wolf);
        }
    }
}
