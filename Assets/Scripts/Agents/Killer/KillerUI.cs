using UnityEngine;
using UnityEngine.UI;

public class KillerUI : MonoBehaviour
{
    [SerializeField] private Image _patrolIcon;
    [SerializeField] private Image _chaseIcon;
    [SerializeField] private Image _trackIcon;

    public void ChangeMode(KillerMode mode)
    {
        _patrolIcon.enabled = false;
        _chaseIcon.enabled = false;
        _trackIcon.enabled = false;

        switch (mode)
        {
            case KillerMode.Patrol:
                _patrolIcon.enabled = true;
                break;
            case KillerMode.Chase:
                _chaseIcon.enabled = true;
                break;
            case KillerMode.Track:
                _trackIcon.enabled = true;
                break;
            default:
                break;
        }
    }
}
