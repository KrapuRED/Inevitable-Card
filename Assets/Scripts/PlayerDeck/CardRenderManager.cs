using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D;

public class CardRenderManager : MonoBehaviour
{
    public static CardRenderManager instance;

    [SerializeField] private int _globalTopOrder = 1000;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ApplyOrder(CardDeck card, int indexLayer)
    {
        if (card == null)
            return;

        SpriteRenderer rootSpriteRenderer = card.GetComponent<SpriteRenderer>();
        if (rootSpriteRenderer == null)
        {
            Debug.Log($"{card.name} in order number {rootSpriteRenderer.sortingLayerID}");
            Debug.LogWarning("There are no rootSpriteRenderer");
            return;
        }

        rootSpriteRenderer.sortingOrder = indexLayer;
        var spriteRenderers = card.GetComponentsInChildren<SpriteRenderer>(true);
        var meshRenderes = card.GetComponentsInChildren<MeshRenderer>(true);

        foreach (var sr in spriteRenderers)
        {
            if (sr == rootSpriteRenderer)
                continue;

            if (sr.gameObject.name == "Illustration")
                sr.sortingOrder = indexLayer - 1;
            else
                sr.sortingOrder = indexLayer + 1;
        }

        foreach (var mr in meshRenderes)
        {
            mr.sortingOrder = indexLayer + 1;
        }
    }

    private int GetNextTopOrder()
    {
        _globalTopOrder += 10;
        return _globalTopOrder;
    }

    public void SetBaseOrder(CardDeck card, int slotIndex)
    {
        int baseOrder = slotIndex * 10;
        card.SetBaseLayerOrder(baseOrder);
        ApplyOrder(card, baseOrder);
    }

    public void OnHoverEnter(CardDeck card)
    {
        ApplyOrder(card, card.BaseLayerOrder + 60);
    }

    public void OnHoverExit(CardDeck card)
    {
        ApplyOrder(card, card.BaseLayerOrder);
    }

    public void OnDragStart(CardDeck card)
    {
        ApplyOrder(card, GetNextTopOrder());
    }

}
