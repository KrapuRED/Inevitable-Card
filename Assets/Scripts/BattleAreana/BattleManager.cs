using System.Collections;
using System.Linq;
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
    [SerializeField] private CardInstance[] EnemyBattleDecks;

    [Header("Events")]
    public OnShowButtonEventSO OnShowButton;
    public OnHideButtonEventSO OnHideButton;
    public OnEnemyPickingCardEventSO onEnemyPickingCardEvent;

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

    private void Start()
    {
        BattleStart();
    }

    public bool ChangeDataPlayerBattleDeck(CardInstance cardData, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= maxSlots)
            return false;

        playerBattleDecks[slotIndex] = cardData;

        if (playerBattleDecks.Length > 0)
        {
            OnShowButton.OnRaiseEvent(UIButtonContext.Battle, true);
        }

        DebugPlayerBattleDeck();
        //PlayerDeckManager.instance.HideItemCard();
        return true;
    }

    public bool ChangeDataEnemyBattleDeck(CardInstance cardData, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= maxSlots)
            return false;

        EnemyBattleDecks[slotIndex] = cardData;

        DebugEnemyBattleDeck();
        return true;
    }

    public void BattleStart()
    {
        //call enemy to stand by the battleDeck
        onEnemyPickingCardEvent.OnRaise(maxSlots);
    }

    public void ReadyForBattle()
    {
        Debug.Log("The Battle BEGGUN");
        for (int i = 0; i < playerBattleDecks.Length; i++)
            playerBattleDecks[i] = null;

        StartCoroutine(DelayDebugLog());
    }

    public void EndBattle()
    {

    }

    #region DEBUGGING AREA
    private void DebugPlayerBattleDeck()
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

    private void DebugEnemyBattleDeck()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            string cardName =
                EnemyBattleDecks[i] != null
                    ? EnemyBattleDecks[i].cardData.cardName
                    : "EMPTY";

            Debug.Log($"{i + 1}. {cardName}, Battle Card Deck {i + 1}");
        }
    }

     IEnumerator DelayDebugLog()
    {
        OnHideButton.OnRaiseEvent(UIButtonContext.Battle, false);
        yield return new WaitForSeconds( 3f );
        DebugPlayerBattleDeck();
    }
    #endregion
}
