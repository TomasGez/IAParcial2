using System;
using UnityEngine;

public class PreyTrigger : FieldOfViewSensor
{
    public event Action<AgentNPC, Enum> OnHunterEnter;
    public event Action<AgentNPC, Enum> OnHunterExit;

    private void Awake()
    {
        _agent = GetComponentInParent<Sheep>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            _target = other.GetComponentInParent<Wolf>();
            
            if (_target == null) return;

            OnHunterEnter?.Invoke(_target, PreyMode.Escape);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            _target = other.GetComponentInParent<Wolf>();

            if (_target == null) return;
                
            OnHunterExit?.Invoke(_target, PreyMode.Flock);
        }
    }
}
