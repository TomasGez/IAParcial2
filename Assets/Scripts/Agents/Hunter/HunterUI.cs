using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HunterUI : MonoBehaviour
{
    [SerializeField] private Image _scoutIcon;
    [SerializeField] private Image _retreatIcon;
    [SerializeField] private Image _restBar;
    [SerializeField] private Image _restBarBackground;
    [SerializeField] private Image _respawnBar;
    [SerializeField] private Image _respawnBarBackground;
    [SerializeField] private Image _huntBar;
    [SerializeField] private Image _huntBarBackground;
    [SerializeField] private Image _gatherBar;
    [SerializeField] private Image _gatherBarBackground;
    [SerializeField] private Image _leaveIcon;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _allCollectedIcon;

    public void ChangeMode(HunterMode mode)
    {
        _scoutIcon.enabled = false;
        _retreatIcon.enabled = false;
        _restBar.enabled = false;
        _restBarBackground.enabled = false;
        _respawnBar.enabled = false;
        _respawnBarBackground.enabled = false;
        _huntBar.enabled = false;
        _huntBarBackground.enabled = false;
        _gatherBar.enabled = false;
        _gatherBarBackground.enabled = false;
        _leaveIcon.enabled = false;

        switch (mode)
        {
            case HunterMode.Scout:
                _scoutIcon.enabled = true;
                break;
            case HunterMode.Retreat:
                _retreatIcon.enabled = true;
                break;
            case HunterMode.Rest:
                _restBar.enabled = true;
                _restBarBackground.enabled = true;
                break;
            case HunterMode.Respawn:
                _respawnBar.enabled = true;
                _respawnBarBackground.enabled = true;
                break;
            case HunterMode.Hunt:
                _huntBar.enabled = true;
                _huntBarBackground.enabled = true;
                break;
            case HunterMode.Gather:
                _gatherBar.enabled = true;
                _gatherBarBackground.enabled = true;
                break;
            case HunterMode.Leave:
                _leaveIcon.enabled = true;
                break;
            default:
                break;
        }
    }

    public void UpdateCollected(int amount, int objective)
    {
        if (amount < objective)
        {
            _allCollectedIcon.enabled = false;
            _amountText.enabled = true;

            _amountText.text = amount.ToString();
        }
        else
        {
            _allCollectedIcon.enabled = true;
            _amountText.enabled = false;
        }
        
    }

    public void UpdateResting(float currentValue, float maxValue, HunterMode mode)
    {
        float barFill = Mathf.Clamp(currentValue / maxValue, 0, 1);

        switch (mode)
        {
            case HunterMode.Rest:
                _restBar.fillAmount = barFill;
                break;
            case HunterMode.Respawn:
                _respawnBar.fillAmount = barFill;
                break;
            case HunterMode.Hunt:
                _huntBar.fillAmount = barFill;
                break;
            case HunterMode.Gather:
                _gatherBar.fillAmount = barFill;
                break;
            default:
                break;
        }
    }
}
