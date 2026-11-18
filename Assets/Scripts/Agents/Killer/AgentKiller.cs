using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentKiller : AgentPathFinding
{
    protected static List<Ghost> _allGhost = new List<Ghost>();
    public Wolf WolfTarget => CurrentTarget as Wolf;
    private int _loopIndex = 0;

    protected void GhostWolfReference(Wolf target)
    {
        foreach (Ghost ghost in _allGhost)
        {
            ghost.TargetEntered(target);
        }
    }

    public void SeekNextNode()
    {
        StopWalkPath();

        Node startNode = _grid.GetAdjacentNode(transform.position, _nodeDetectionDistance);

        if (startNode == null)
        {
            _phantomNode = _grid.NewPhantomNode(transform.position);
            startNode = _phantomNode;
        }

        Node endNode = WolfTarget.NextNode;

        List<Node> path = PathFinder<Node>.AStarSearch(startNode, endNode, node => node.Neighbors,
                                                       node => node.DangerNodeCost(),
                                                       node => node.transform.position, true);

        if (_walkingRoutine != null) StopCoroutine(_walkingRoutine);
        _walkingRoutine = StartCoroutine(WalkTargetPath(path));
    }

    private IEnumerator WalkTargetPath(List<Node> path)
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

        _stateMachine.ChangeState(KillerMode.Patrol);
    }

    public void SeekLoop(List<Node> loopNodes)
    {
        StopWalkPath();

        Node startNode = _grid.GetAdjacentNode(transform.position, _nodeDetectionDistance);

        if (startNode == null)
        {
            _phantomNode = _grid.NewPhantomNode(transform.position);
            startNode = _phantomNode;
        }

        Node endNode = loopNodes[_loopIndex];

        List<Node> path = PathFinder<Node>.AStarSearch(startNode, endNode, node => node.Neighbors,
                                                       node => node.DangerNodeCost(),
                                                       node => node.transform.position, false);

        if (_walkingRoutine != null) StopCoroutine(_walkingRoutine);
        _walkingRoutine = StartCoroutine(WalkLoopPath(path, loopNodes));
    }
    
    private IEnumerator WalkLoopPath(List<Node> path, List<Node> loopNodes)
    {
        foreach (Node node in path)
        {
            while ((transform.position - node.transform.position).sqrMagnitude > 0.01f)
            {
                Seek(node.transform.position);
                yield return null;
            }
        }

        _loopIndex = (_loopIndex + 1) % loopNodes.Count;

        yield return null;

        SeekLoop(loopNodes);
    }
}
