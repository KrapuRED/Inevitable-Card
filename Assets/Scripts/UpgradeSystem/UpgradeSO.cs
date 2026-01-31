using UnityEngine;

public enum UpgradeType
{
    MaxHealth,
    BaseDamage,
    MaxStamina
}

[CreateAssetMenu(fileName = "Upgrade", menuName = "UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    public Sprite upgradeImage;
    public UpgradeType upgradeType;
    public float upgradeValue; 
}
