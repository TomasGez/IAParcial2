using System;
using UnityEngine;

public class PreyTrigger : FieldOfViewSensor
{
    public event Action<Agent> OnHunterEnter;
    public event Action<Agent> OnHunterExit;

    private void Awake()
    {
        _agent = GetComponentInParent<Sheep>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            Wolf target = other.GetComponentInParent<Wolf>();
            
            if (target == null) return;

            OnHunterEnter?.Invoke(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            Wolf target = other.GetComponentInParent<Wolf>();

            if (target == null) return;
                
            OnHunterExit?.Invoke(target);
        }
    }
}
