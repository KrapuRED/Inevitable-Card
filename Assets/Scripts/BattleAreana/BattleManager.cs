using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Config Battle Arena")]
    private int maxSlots;
    public GameObject playerBattleContainer;
    [SerializeField] private BattleCardDeck[] playerBattleCardDecks;
    [SerializeField] private CardInstance[] playerBattleDecks;
    public GameObject EnemyBattleDeck;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        maxSlots = playerBattleContainer.transform.childCount;
        playerBattleCardDecks = playerBattleContainer.GetComponentsInChildren<BattleCardDeck>();

        for (int i = 0; i < playerBattleCardDecks.Length; i++)
            playerBattleCardDecks[i].Init(i);

        playerBattleDecks = new CardInstance[maxSlots];
    }

    public bool ChangeDataPlayerBattleDeck(CardInstance cardData, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= maxSlots)
            return false;

        playerBattleDecks[slotIndex] = cardData;

        if (playerBattleDecks.Length > 0)
        {
            //Call UI to enable to initianet move
            Debug.Log("enable to initianet move");
        }

        //DebugBattleSlots();
        //PlayerDeckManager.instance.HideItemCard();
        return true;
    }

    public void ReadyForBattle()
    {
       
    }

    private void DebugBattleSlots()
    {

        for (int i = 0; i < maxSlots; i++)
        {
            string cardName =
                playerBattleDecks[i] != null
                    ? playerBattleDecks[i].cardData.cardName
                    : "EMPTY";

            Debug.Log($"{i + 1}. {cardName}, Battle Card Deck {i + 1}");
        }
    }
}
