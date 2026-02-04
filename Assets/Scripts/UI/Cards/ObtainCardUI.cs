using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObtainCardUI : MonoBehaviour
{
    public Image illustration;

    public void SetImageCard(CardSO cardData)
    {
        illustration.sprite = cardData.cardImage;
        gameObject.SetActive(true);
    }

    public void ResetObtainCardUI()
    {
        gameObject.SetActive(false);
    }
}
