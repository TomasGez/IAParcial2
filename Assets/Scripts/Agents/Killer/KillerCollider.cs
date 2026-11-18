using UnityEngine;

public class KillerCollider : KillerTrigger
{
    private void OnCollisionEnter(Collision collision)
    {
        if (IsValidTarget(collision.gameObject.layer, _agent.TargetMask))
        {
            if (collision.gameObject.TryGetComponent<Wolf>(out var wolf)) wolf.WasKilled(true);
        }
    }
}
