using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDBorderCard : MonoBehaviour
{
    [Header("reference")]
    public Slider healthBar;
    public TextMeshProUGUI healthValueText;
    public TextMeshProUGUI staminaValueText;
    public TextMeshProUGUI usedStaminaValueText;
    public TextMeshProUGUI baseDamageValue;

    public void SetHealth(float current, float max)
    {
        healthBar.value = current / max;
        healthValueText.text = $"{current}";
    }

    public void SetUsedStamina(int used)
    {
        usedStaminaValueText.text = used > 0 ? $"-{used}" : "";
    }

    public void SetBaseDamage(float baseDamage)
    {
        baseDamageValue.text = baseDamage.ToString();
    }

    public void SetStamina(int currentStaminaValue, int UsedStaminaValue)
    {
        int remaining = currentStaminaValue - UsedStaminaValue;

        staminaValueText.text = remaining.ToString();
    }
}
