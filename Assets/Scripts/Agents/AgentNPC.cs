using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPathFinding : Agent
{
    [Header("Path Finding")]
    [SerializeField] private Graph _grid;
    public Node NextNode { get; private set; }
    private Coroutine _walkingRoutine;



    private void SearchPath(Node startNode, Node endNode)
    {
        List<Node> path = PathFinder<Node>.AStarSearch(startNode, endNode, node => node.Neighbors,
                                                       node => node.DangerNodeCost(),
                                                       node => node.transform.position, true);
        
        if (_walkingRoutine != null) StopCoroutine(_walkingRoutine);
        _walkingRoutine = StartCoroutine(WalkPath(path));
    }

    private void Seek(Node target)
    {
        Vector3 desiredVelocity = CalculatedDirection(target.transform.position);

        _currentVelocity += CalculatedSteering(desiredVelocity);
    }

    private IEnumerator WalkPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            NextNode = node;

            while ((transform.position - node.transform.position).sqrMagnitude > 0.001f)
            {
                Seek(node);
                yield return null;
            }
        }
    }
}
