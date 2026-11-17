using System;
using UnityEngine;

public class HunterTrigger : FieldOfViewSensor
{
    public event Action<Sheep> OnPreyEnter;
    public event Action<Sheep, float> OnPreyExit;

    private void Awake()
    {
        _agent = GetComponentInParent<Wolf>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            if (other.TryGetComponent<Sheep>(out var sheep)) OnPreyEnter?.Invoke(sheep);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            if (other.TryGetComponent<Sheep>(out var sheep)) OnPreyExit?.Invoke(sheep, _agent.KeepTargetTime);
        }
    }
}
