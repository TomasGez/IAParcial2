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

        if (localPos.x > _collider.size.x / 2) localPos.x -= _collider.size.x;
        if (localPos.x < -_collider.size.x / 2) localPos.x += _collider.size.x;
        if (localPos.z > _collider.size.z / 2) localPos.z -= _collider.size.z;
        if (localPos.z < -_collider.size.z / 2) localPos.z += _collider.size.z;

        return transform.TransformPoint(localPos);
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_targetMask.value & (1 << other.gameObject.layer)) != 0)
        {
            if (_zoneType == Zone.Hunting)
            {
                if (other.TryGetComponent<Sheep>(out var sheep))
                {
                    if (sheep.WolfTarget != null) sheep.WolfTarget.TargetExited(sheep);

                    sheep.ClearTargets();
                    sheep.transform.position = Wraparound(sheep.transform.position);
                }
            }

            if (_zoneType == Zone.Start || _zoneType == Zone.Safe || _zoneType == Zone.Goal)
            {
                if (other.TryGetComponent<Wolf>(out var wolf)) wolf.InSafeZone(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_targetMask.value & (1 << other.gameObject.layer)) != 0)
        {
            if (_zoneType == Zone.Start || _zoneType == Zone.Safe || _zoneType == Zone.Goal)
            {
                if (other.TryGetComponent<Wolf>(out var wolf)) wolf.InSafeZone(true);
            }
        }
    }
}
