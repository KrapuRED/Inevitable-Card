using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public UpgradeSO upgradeData;

    public Image imageUpgrade;
    public TextMeshProUGUI textUpgrade;

    private void Start()
    {
        imageUpgrade.sprite = upgradeData.upgradeImage;
        textUpgrade.text = upgradeData.upgradeName;
    }
}
