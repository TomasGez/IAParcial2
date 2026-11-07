using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    [Header("Node Data")]
    [SerializeField] private List<Node> _neighbors = new();
    [SerializeField] private Zone _nodeType;
    [SerializeField] private Image _nodeTypeIcon;
    private bool isDangerNode = false;

    public List<Node> Neighbors => _neighbors;
    public Zone NodeType => _nodeType;

    private void OnEnable()
    {
        _nodeTypeIcon.enabled = false;
    }

    public void IsCurrentObjective(bool active)
    {
        if (_nodeType != Zone.None)
            _nodeTypeIcon.enabled = active;
    }

    public void IsDangerNode()
    {
        isDangerNode = true;
    }

    public int DangerNodeCost()
    {
        if (isDangerNode) return 2;

        return 0;
    }
}
