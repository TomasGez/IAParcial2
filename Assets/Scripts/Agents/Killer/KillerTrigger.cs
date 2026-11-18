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

// using System;
// using UnityEngine;

// public class KillerTrigger : FieldOfViewSensor
// {
//     public event Action<Wolf> OnHunterEnter;

//     private Wolf _wolfInRange;

//     private void Awake()
//     {
//         _agent = GetComponentInParent<Ghost>();
//     }

//     private void Update()
//     {
//         if (_wolfInRange == null)
//         {
//             _canSeeTarget = false;
//             return;
//         }

//         _target = _wolfInRange;
//         bool couldSee = _canSeeTarget;
//         _canSeeTarget = InViewAngle() && InLineOfSight();

//         // Solo dispara el evento cuando pasa de no verlo a verlo
//         if (_canSeeTarget && !couldSee)
//         {
//             Debug.Log("Hola");
//             OnHunterEnter?.Invoke(_wolfInRange);
//         }
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask)) return;

//         if (other.TryGetComponent<Wolf>(out var wolf))
//             _wolfInRange = wolf;
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (!IsValidTarget(other.gameObject.layer, _agent.TargetMask)) return;

//         if (other.TryGetComponent<Wolf>(out var wolf) && wolf == _wolfInRange)
//         {
//             _wolfInRange = null;
//             _target = null;
//             _canSeeTarget = false;
//         }
//     }
// }