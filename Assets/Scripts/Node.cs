using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    [SerializeField] private List<Node> _neighbors = new();
    [SerializeField] private Zone _nodeType;
    [SerializeField] private Image _nodeTypeIcon;
    [SerializeField] private Texture2D _iconTexture;

    public IReadOnlyList<Node> Neighbors => _neighbors;
    public Zone NodeType => _nodeType;

    private void OnEnable()
    {
        if (_iconTexture != null)
        {
            _nodeTypeIcon.sprite = Sprite.Create(_iconTexture,
            new Rect(0, 0, _iconTexture.width, _iconTexture.height),
            new Vector2(0.5f, 0.5f));
        }
    }

    public void IsCurrentObjective(bool active)
    {
        _nodeTypeIcon.enabled = active;
    }
}
