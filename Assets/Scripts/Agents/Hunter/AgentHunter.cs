using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentHunter : AgentPathFinding
{
    public Sheep SheepTarget => CurrentTarget as Sheep;
    public int CollectedPreys { get; protected set; }
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

    public void SeekClosestNode(Zone type)
    {
        StopWalkPath();

        List<Node> validNodes = _grid.GetNodesByType(type);

        Node startNode = _grid.GetAdjacentNode(transform.position, _nodeDetectionDistance);

        if (startNode == null)
        {
            _phantomNode = _grid.NewPhantomNode(transform.position);
            startNode = _phantomNode;
        }

        Node endNode = _grid.GetClosestNode(transform.position, validNodes);

        List<Node> path = PathFinder<Node>.AStarSearch(startNode, endNode, node => node.Neighbors,
                                                       node => node.DangerNodeCost(),
                                                       node => node.transform.position, true);

        if (_walkingRoutine != null) StopCoroutine(_walkingRoutine);
        _walkingRoutine = StartCoroutine(WalkFastestPath(path, type));
    }

    private IEnumerator WalkFastestPath(List<Node> path, Zone type)
    {
        foreach (Node node in path)
        {
            while ((transform.position - node.transform.position).sqrMagnitude > 0.01f)
            {
                Seek(node.transform.position);
                yield return null;
            }
        }

        yield return null;

        if (type == Zone.Safe) _stateMachine.ChangeState(HunterMode.Rest);

        else if (type == Zone.Goal) _stateMachine.ChangeState(HunterMode.Respawn);
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

                Seek(node.transform.position);
                yield return null;
            }
        }

        yield return null;

        SeekRandom();
    }

    public void Pursuit(Agent target, float slowingDistance, float minDistance)
    {
        Vector3 desiredVelocity = CalculatedDirection(CalculatedFuture(target));

        _currentVelocity += CalculatedSteering(desiredVelocity);

        float distance = Vector3.Distance(target.transform.position, transform.position);

        if(distance <= minDistance)
        {
            _currentVelocity = Vector3.zero;
            return;
        }

        float stoppingDistance = distance - minDistance;
        float targetSpeed = _maxSpeed * (stoppingDistance / slowingDistance);
        _currentSpeed = Mathf.Min(targetSpeed, _maxSpeed);
    }

    public void PreyCollected()
    {
        CollectedPreys ++;
    }

    public void ResetCollection()
    {
        CollectedPreys = 0;
    }

    public void RestartFromHome()
    {
        List<Node> validNodes = _grid.GetNodesByType(Zone.Start);

        Node startNode = _grid.GetClosestNode(transform.position, validNodes);

        transform.position = startNode.transform.position;
    }
}
