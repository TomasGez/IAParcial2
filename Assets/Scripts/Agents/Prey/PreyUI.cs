using UnityEngine;
using UnityEngine.UI;

public class PreyUI : MonoBehaviour
{
    [SerializeField] private Image _flockIcon;
    [SerializeField] private Image _escapeIcon;
    [SerializeField] private Image _deadIcon;

    public void ChangeMode(PreyMode mode)
    {
        _flockIcon.enabled = false;
        _escapeIcon.enabled = false;
        _deadIcon.enabled = false;

        switch (mode)
        {
            case PreyMode.Flock:
                _flockIcon.enabled = true;
                break;
            case PreyMode.Escape:
                _escapeIcon.enabled = true;
                break;
            case PreyMode.Dead:
                _deadIcon.enabled = true;
                break;
            default:
                break;
        }
    }
}
