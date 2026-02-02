using UnityEngine;
using System.Collections.Generic;

public enum DeckDirection
{
    Right,
    Left,
    Center
}

public class DeckCardPositioner : MonoBehaviour
{
    public DeckDirection direction;
    public float cardWidth;
    public float overlap;
    public float ypos;

    public void RepositionCards(List<CardDeck> cards)
    {
        float spacing = cardWidth - overlap;

        float startX = 0f;

        if (direction == DeckDirection.Left)
        {
            startX = 0f;
        }
        else if (direction == DeckDirection.Center)
        {
            float totalWidth = spacing * (cards.Count - 1);
            startX = -totalWidth / 2f;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            float dir = direction == DeckDirection.Left ? -1f : 1f;
            float xPos = startX + i * spacing * dir;

            //Debug.Log($"{cardDatas[i].cardData.cardName} is Reposiotion to {xPos}");
            CardRenderManager.instance.SetBaseOrder(cards[i], i);

            cards[i].transform.localPosition =
                new Vector3(xPos, ypos, -i * 0.01f);
        }
    }
}
