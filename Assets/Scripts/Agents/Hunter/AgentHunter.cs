using UnityEngine;

public abstract class AgentHunter : AgentNPC
{
    public void Seek(AgentNPC target)
    {
        Vector3 desiredVelocity = CalculatedDirection(target.transform.position);

        _currentVelocity += CalculatedSteering(desiredVelocity);
    }

    public void Pursuit(AgentNPC target)
    {
        Vector3 desiredVelocity = CalculatedDirection(CalculatedFuture(target));

        _currentVelocity += CalculatedSteering(desiredVelocity);
    }
}
