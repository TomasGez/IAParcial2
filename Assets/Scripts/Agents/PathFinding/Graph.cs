using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [Header("Grid Data")]
    [SerializeField] private List<Node> _allNodes = new();
    [SerializeField] private Node _nodePrefab;
    [SerializeField] private LayerMask _obstacleMask;
    
    public IReadOnlyList<Node> AllNodes => _allNodes;

    public Node GetRandomNode(List<Node> nodeList, Node excluded)
    {
        if (nodeList.Count == 1) return nodeList[0];

        Node selectedNode;

        do
        {
            int randomIndex = Random.Range(0, nodeList.Count);
            selectedNode = nodeList[randomIndex];
        } 
        while (selectedNode == excluded);

        return selectedNode;
    }

    public List<Node> GetNodesByType(Zone type)
    {
        List<Node> filteredNodes = new List<Node>();

        foreach (Node node in _allNodes)
        {
            if (node.NodeType == type)
            {
                filteredNodes.Add(node);
            }
        }

        return filteredNodes;
    }

    public Node GetClosestNode(Vector3 position, List<Node> nodes)
    {
        Node closestNode = null;
        float minSqrDistance = float.MaxValue;

        foreach (Node node in nodes)
        {
            float sqrDist = (node.transform.position - position).sqrMagnitude;

            if (sqrDist < minSqrDistance)
            {
                minSqrDistance = sqrDist;
                closestNode = node;
            }
        }

        return closestNode;
    }

    public Node GetAdjacentNode(Vector3 position, float maxDistance)
    {
        Node closestNode = null;
        float minSqrDistance = maxDistance * maxDistance;

        foreach (Node node in _allNodes)
        {
            float sqrDist = (node.transform.position - position).sqrMagnitude;

            if (sqrDist < minSqrDistance)
            {
                minSqrDistance = sqrDist;
                closestNode = node;
            }
        }

        return closestNode;
    }

    public Node NewPhantomNode(Vector3 agentPosition)
    {
        Node phantom = Instantiate(_nodePrefab, agentPosition, Quaternion.identity);
        phantom.name = "Phantom Node";

        FindNeighbors(phantom);

        if (phantom.Neighbors == null || phantom.Neighbors.Count == 0)
        {
            Node closestGlobalNode = GetClosestNode(agentPosition, _allNodes);

            if (closestGlobalNode != null)
            {
                phantom.Neighbors.Add(closestGlobalNode);

                if (!closestGlobalNode.Neighbors.Contains(phantom)) closestGlobalNode.Neighbors.Add(phantom);
            }
        }

        return phantom;
    }

    private void FindNeighbors(Node newNode)
    {
        foreach (Node node in _allNodes)
        {
            if (!Physics.Linecast(newNode.transform.position + Vector3.up, node.transform.position + Vector3.up, _obstacleMask))
                newNode.Neighbors.Add(node);
        }
    }
}
