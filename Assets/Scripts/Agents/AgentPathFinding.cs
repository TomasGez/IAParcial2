using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPathFinding : Agent
{
    [Header("Path Finding")]
    [SerializeField] protected Graph _grid;
    [SerializeField] protected float _nodeDetectionDistance = 2f;
    protected Node _phantomNode;
    protected Coroutine _walkingRoutine;

    public virtual void StopWalkPath()
    {
        if (_walkingRoutine != null)
        {
            StopCoroutine(_walkingRoutine);
            _walkingRoutine = null;
        }

        if (_phantomNode != null)
        {
            Destroy(_phantomNode.gameObject);
            _phantomNode = null;
        }
    }

    protected void Seek(Node target)
    {
        Vector3 desiredVelocity = CalculatedDirection(target.transform.position);

        _currentVelocity += CalculatedSteering(desiredVelocity);
    }
}
