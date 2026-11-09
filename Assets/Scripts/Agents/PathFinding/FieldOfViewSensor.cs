using UnityEngine;

public class FieldOfViewSensor : MonoBehaviour
{
    protected Agent _agent;
    protected Agent _target;
    protected bool _canSeeTarget;
    public bool CanSeeTarget => _canSeeTarget;

    protected static bool IsValidTarget(int layer, LayerMask mask) => (mask.value & (1 << layer)) != 0;

    protected bool InViewAngle()
    {
        Vector3 dirToTarget = (_target.transform.position - _agent.transform.position).normalized;
        return Vector3.Angle(_agent.transform.forward, dirToTarget) <= _agent.ViewAngle * 0.5f;
    }

    protected bool InLineOfSight()
    {
        Vector3 eyePos = _agent.transform.position + Vector3.up * _agent.EyeHeight;
        Vector3 targetPos = _target.transform.position + Vector3.up * _target.EyeHeight;
        Vector3 direction = targetPos - eyePos;

        if(Physics.Raycast(eyePos, direction.normalized, out RaycastHit hit, direction.magnitude, _agent.ObstacleMask))
        {
            return hit.transform == _target || hit.transform.IsChildOf(_target.transform);
        }

        return true;
    }
}
