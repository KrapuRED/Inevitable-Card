using UnityEngine;

public enum StatusType
{
    Health,
    Stamina
}

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [Header("Reference")]
    public HUDBorderCard playerCard;
    public HUDBorderCard enemyCard;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    // Called when selecting/deselecting cards
    public void UpdatePlayerStaminaPreview(int usedStamina)
    {
        playerCard.SetUsedStamina(usedStamina);
    }

    // Called AFTER pressing Battle button (stamina actually spent)
    public void CommitPlayerStamina(int newCurrentStamina)
    {
        playerCard.SetStamina(newCurrentStamina, 0);
        playerCard.SetUsedStamina(0);
    }

    public void UpdatePlayerHealth(float currentHealtPoint, float maxHealtPoint)
    {
        playerCard.SetHealth(currentHealtPoint, maxHealtPoint);
    }

    public void UpdateEnemyHealth(float currentHealtPoint, float maxHealtPoint)
    {
        enemyCard.SetHealth(currentHealtPoint, maxHealtPoint);
    }

}
