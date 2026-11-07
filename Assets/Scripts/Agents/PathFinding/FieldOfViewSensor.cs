using UnityEngine;

public class FieldOfViewSensor : MonoBehaviour
{
    protected Agent target;

    [Header("View Range")]
    [SerializeField] protected float viewRadius;
    [SerializeField, Range(0f, 360f)] protected float viewAngle = 90f;
    [SerializeField] protected float eyeHeight = 1f;

    [Header("Layers")]
    [SerializeField] protected LayerMask targetMask = ~0;
    [SerializeField] protected LayerMask obstacleMask = ~0;

    public bool CanSeeTarget {get; protected set;}
    public Agent Target => target;
    public float ViewRadius => viewRadius;
    public float ViewAngle => viewAngle;
    public float EyeHeight => eyeHeight;
    public LayerMask ObstacleMask => obstacleMask;

    protected static bool IsValidTarget(int layer, LayerMask mask) => (mask.value & (1 << layer)) != 0;

    protected bool InViewAngle()
    {
        Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
        return Vector3.Angle(transform.forward, dirToTarget) <= viewAngle * 0.5f;
    }

    protected bool InLineOfSight()
    {
        Vector3 eyePos = transform.position + Vector3.up * eyeHeight;
        Vector3 targetPos = target.transform.position + Vector3.up * eyeHeight;
        Vector3 direction = targetPos - eyePos;

        if(Physics.Raycast(eyePos, direction.normalized, out RaycastHit hit, direction.magnitude, obstacleMask))
        {
            return hit.transform == target || hit.transform.IsChildOf(target.transform);
        }

        return true;
    }
}
