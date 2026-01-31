using UnityEngine;

public class PlayerUpgradeHandler : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;

    private void Start()
    {
        _player = GetComponentInParent<PlayerCharacter>();
    }

    public void ApplyUpgrade(UpgradeSO upgrade)
    {
        Debug.Log($"Upgrade : {upgrade.upgradeName} Value : {upgrade.upgradeValue} ");
        switch (upgrade.upgradeType)
        {
            case UpgradeType.MaxHealth:
                _player.maxHealtPoint += upgrade.upgradeValue;
                DamageManager.instance.HealToTarget(TargetType.Player, (int) upgrade.upgradeValue);
                break;

            case UpgradeType.MaxStamina:
                _player.maxStamina += (int) upgrade.upgradeValue;
                break;

            case UpgradeType.BaseDamage:
                _player.baseDamage += upgrade.upgradeValue;

                break;
        }
    }
}
