using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    [SerializeField] private List<Node> _neighbors = new();
    [SerializeField] private Zone _nodeType;
    [SerializeField] private Image _nodeTypeIcon;

    public IReadOnlyList<Node> Neighbors => _neighbors;
    public Zone NodeType => _nodeType;

    private void OnEnable()
    {
        _nodeTypeIcon.enabled = false;
    }

    public void IsCurrentObjective(bool active)
    {
        _nodeTypeIcon.enabled = active;
    }
}
