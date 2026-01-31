using UnityEngine;

public class UpgradeButton : CostumizeButton
{
    public UpgradeUI upgradeUI;

    public override void OnClickButton()
    {
        UpgradeManager.instance.UpgradePlayerStatus(upgradeUI.upgradeData);
    }
}
