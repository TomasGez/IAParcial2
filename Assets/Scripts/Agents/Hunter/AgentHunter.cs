using UnityEngine;

public abstract class AgentHunter : AgentPathFinding
{
    public void Pursuit(Agent target)
    {
        Vector3 desiredVelocity = CalculatedDirection(CalculatedFuture(target));

        _currentVelocity += CalculatedSteering(desiredVelocity);
    }
}
