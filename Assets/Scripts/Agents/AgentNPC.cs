using UnityEngine;

public class AgentNPC : Agent
{
    protected Vector3 CalculatedDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction *= _currentSpeed;

        return direction;
    }

    protected Vector3 CalculatedSteering(Vector3 desiredDirection)
    {
        Vector3 steering =  desiredDirection - _currentVelocity;

        steering = Vector3.ClampMagnitude(steering, _maxSteering * Time.deltaTime);

        return steering;
    }

    protected Vector3 CalculatedFuture(AgentNPC target)
    {
        Vector3 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;

        float prediction = distance / (_maxSpeed + target._currentVelocity.magnitude);
        Vector3 futurePosition = target.transform.position + target._currentVelocity * prediction;
        return futurePosition;
    }
}
