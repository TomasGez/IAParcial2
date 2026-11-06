using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [Header("Bounds")]
    [SerializeField] BoxCollider _collider;
    [SerializeField] private Zone _zoneType;
    [SerializeField] private LayerMask _targetMask = ~0;

    public Vector3 Wraparound(Vector3 position)
    {
        Vector3 localPos = transform.InverseTransformPoint(position);

        if (localPos.x > _collider.size.x / 2) localPos.x -= _collider.size.x + 0.5f;
        if (localPos.x < -_collider.size.x / 2) localPos.x += _collider.size.x + 0.5f;
        if (localPos.z > _collider.size.z / 2) localPos.z -= _collider.size.z + 0.5f;
        if (localPos.z < -_collider.size.z / 2) localPos.z += _collider.size.z + 0.5f;

        return transform.TransformPoint(localPos);
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_targetMask.value & (1 << other.gameObject.layer)) != 0)
        {
            if (_zoneType == Zone.Hunting)
            {
                AgentPrey target = other.GetComponentInParent<AgentPrey>();
                target.ClearTargets();

                other.gameObject.transform.position = Wraparound(other.gameObject.transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_targetMask.value & (1 << other.gameObject.layer)) != 0)
        {
            if (_zoneType == Zone.Safe)
            {
                
            }

            if (_zoneType == Zone.Goal)
            {
                
            }
        }
    }
}
