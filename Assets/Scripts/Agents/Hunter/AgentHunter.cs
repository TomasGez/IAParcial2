using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentHunter : AgentPathFinding
{
    protected Node _currentEndNode;
    public Node NextNode { get; protected set; }

    public override void StopWalkPath()
    {
        base.StopWalkPath();

        if (_currentEndNode != null)
        {
            _currentEndNode.IsCurrentObjective(false);
            _currentEndNode = null;
        }
    }

    public void SeekRandom()
    {
        StopWalkPath();

        List<Node> validNodes = _grid.GetNodesByType(Zone.Hunting);

        Node startNode = _grid.GetAdjacentNode(transform.position, _nodeDetectionDistance);

        if (startNode == null)
        {
            _phantomNode = _grid.NewPhantomNode(transform.position);
            startNode = _phantomNode;
        }

        Node endNode = _grid.GetRandomNode(validNodes, startNode);
        _currentEndNode = endNode;

        List<Node> path = PathFinder<Node>.AStarSearch(startNode, endNode, node => node.Neighbors,
                                                       node => node.DangerNodeCost(),
                                                       node => node.transform.position, false);

        if (_walkingRoutine != null) StopCoroutine(_walkingRoutine);
        _walkingRoutine = StartCoroutine(WalkRandomPath(path));
    }

    private IEnumerator WalkRandomPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            NextNode = node;

            while ((transform.position - node.transform.position).sqrMagnitude > 0.01f)
            {
                if (!_currentEndNode.NodeTypeIcon.enabled)
                    _currentEndNode.IsCurrentObjective(true);

                Seek(node);
                yield return null;
            }
        }

        yield return null;

        SeekRandom();
    }

    public void Pursuit(Agent target)
    {
        Vector3 desiredVelocity = CalculatedDirection(CalculatedFuture(target));

        _currentVelocity += CalculatedSteering(desiredVelocity);
    }
}
