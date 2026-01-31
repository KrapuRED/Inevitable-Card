using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public PlayerUpgradeHandler playerUpgradeHandler;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpgradePlayerStatus(UpgradeSO upgradeData)
    {
        playerUpgradeHandler.ApplyUpgrade(upgradeData);
        HUDManager.instance.ClosePanel();
    }
}
