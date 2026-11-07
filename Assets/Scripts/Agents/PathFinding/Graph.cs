using System;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [Header("Grid Data")]
    [SerializeField] private List<Node> _allNodes = new();
    [SerializeField] private Node _nodePrefab;
    [SerializeField] private LayerMask _obstacleMask;
    
    public IReadOnlyList<Node> AllNodes => _allNodes;

    public Node NewPhantomNode(Transform agentTransform)
    {
        Node phantom = Instantiate(_nodePrefab, agentTransform);

        phantom.name = $"Node [{phantom.transform.position.x},{phantom.transform.position.x}]";
        FindNeighbors(phantom);

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
