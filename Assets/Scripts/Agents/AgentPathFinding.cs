using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPathFinding : Agent
{
    [Header("Path Finding")]
    [SerializeField] protected Graph _grid;
    [SerializeField] protected float _nodeDetectionDistance = 2f;
    public Node NextNode { get; protected set; }
    protected Node _phantomNode;
    protected Coroutine _walkingRoutine;

    public void StopWalkPath()
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

    private IEnumerator WalkPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            NextNode = node;

            while ((transform.position - node.transform.position).sqrMagnitude > 0.01f)
            {
                Seek(node);
                yield return null;
            }
        }
    }

    protected void Seek(Node target)
    {
        Vector3 desiredVelocity = CalculatedDirection(target.transform.position);

        _currentVelocity += CalculatedSteering(desiredVelocity);
    }
}
