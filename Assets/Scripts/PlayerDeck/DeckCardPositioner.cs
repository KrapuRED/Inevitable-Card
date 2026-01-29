using UnityEngine;
using System.Collections.Generic;

public class DeckCardPositioner : MonoBehaviour
{
    public float cardWidth;
    public float overlap;
    public float ypos;

    public void RepositionCards(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            float xPos = i  * (cardWidth - overlap);
            Debug.Log($"psotion {xPos}");
            cards[i].transform.localPosition = new Vector3(xPos, ypos , -i * 0.01f);
        }
    }
}
