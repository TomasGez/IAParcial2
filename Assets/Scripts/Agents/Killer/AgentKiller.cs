using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentKiller : AgentPathFinding
{
    protected static List<Ghost> _allGhost = new List<Ghost>();
    private int _loopIndex = 0;

    public void SeekNextNode(AgentHunter target)
    {
        StopWalkPath();

        Node startNode = _grid.GetAdjacentNode(transform.position, _nodeDetectionDistance);

        if (startNode == null)
        {
            _phantomNode = _grid.NewPhantomNode(transform.position);
            startNode = _phantomNode;
        }

        Node endNode = target.NextNode;

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
                Seek(node);
                yield return null;
            }
        }

        yield return null;

        CurrentTarget = null;
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
                                                       node => node.transform.position, true);

        if (_walkingRoutine != null) StopCoroutine(_walkingRoutine);
        _walkingRoutine = StartCoroutine(WalkLoopPath(path, loopNodes));
    }
    
    private IEnumerator WalkLoopPath(List<Node> path, List<Node> loopNodes)
    {
        foreach (Node node in path)
        {
            while ((transform.position - node.transform.position).sqrMagnitude > 0.01f)
            {
                Seek(node);
                yield return null;
            }
        }

        _loopIndex = (_loopIndex + 1) % loopNodes.Count;

        yield return null;

        SeekLoop(loopNodes);
    }
}
