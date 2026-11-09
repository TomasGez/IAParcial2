using System.Collections.Generic;
using UnityEngine;

public abstract class AgentPrey : Agent
{  
    protected static List<Sheep> _allPreys = new List<Sheep>();

    public void RandomDirection()
    {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f,Random.Range(-1f, 1f));
        _currentVelocity = direction.normalized * _currentSpeed;
    }

    private Vector3 CalculatedSeparation(float radius)
    {
        Vector3 desiredVector = default;
        int count = 0;

        foreach(var item in _allPreys)
        {
            if(item == this) continue;

            if(Vector3.Distance(item.transform.position, transform.position) <= radius)
            {
                desiredVector += item.transform.position - transform.position;
                count++;
            }
        }

        if(count == 0) return Vector3.zero;

        desiredVector /= count;
        return CalculatedSteering(-desiredVector.normalized * _maxSpeed);
    }

    private Vector3 CalculatedAlignment(float radius)
    {
        Vector3 desiredVector = default;
        int count = 0;

        foreach(var item in _allPreys)
        {
            if(item == this) continue;

            if(Vector3.Distance(item.transform.position, transform.position) <= radius)
            {
                desiredVector += item._currentVelocity;
                count++;
            }
        }

        if(count == 0) return Vector3.zero;

        desiredVector /= count;
        return CalculatedSteering(desiredVector.normalized * _maxSpeed);
    }

    private Vector3 CalculatedCohesion(float radius)
    {
        Vector3 desiredVector = default;
        int count = 0;

        foreach(var item in _allPreys)
        {
            if(item == this) continue;

            if(Vector3.Distance(item.transform.position, transform.position) <= radius)
            {
                desiredVector += item.transform.position;
                count++;
            }
        }

        if(count == 0) return Vector3.zero;

        desiredVector /= count;
        Vector3 desiredVelocity = CalculatedDirection(desiredVector);
        return CalculatedSteering(desiredVelocity.normalized * _maxSpeed);
    }

    public void Flocking(float sepRadius, float sepWeight, float aliRadius, float aliWeight, float cohRadius, float cohWeight)
    {
        _currentVelocity += CalculatedSeparation(sepRadius) * sepWeight + 
                            CalculatedAlignment(aliRadius) * aliWeight + 
                            CalculatedCohesion(cohRadius) * cohWeight;
    }

    public void Evade(Agent target)
    {
        Vector3 desiredVelocity = CalculatedDirection(CalculatedFuture(target));

        _currentVelocity += CalculatedSteering(-desiredVelocity);
    }
}
