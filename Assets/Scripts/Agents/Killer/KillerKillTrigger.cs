using UnityEngine;

public class KillerKillTrigger : FieldOfViewSensor
{
    private void Awake()
    {
        _agent = GetComponentInParent<Ghost>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other.gameObject.layer, _agent.TargetMask))
        {
            Debug.Log("Hola");
            if (other.TryGetComponent<Wolf>(out var wolf)) wolf.WasKilled(true);
        }
    }
}
