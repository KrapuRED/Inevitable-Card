using UnityEngine;
using System.Collections.Generic;

public class DeckCardPositioner : MonoBehaviour
{
    public float cardWidth;
    public float overlap;

    public void RepositionCards(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            float xPos = i  * (cardWidth - overlap);
            cards[i].transform.localPosition = new Vector3(xPos, cards[i].transform.position.y , -i * 0.01f);
        }
    }
}
