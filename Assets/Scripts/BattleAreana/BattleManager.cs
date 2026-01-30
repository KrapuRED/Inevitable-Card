using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Config Battle Arena")]
    private int maxSlots;
    public GameObject playerBattleContainer;
    private PlayerBattleCardDeck[] playerBattleCardDecks;
    [SerializeField] private CardInstance[] playerBattleDecks;
    [SerializeField] private CardInstance[] EnemyBattleDecks;

    [Header("State Battle Arena")]
    //Player Script and Data
    [SerializeField] private Enemy currentEnemy;
    [SerializeField] private int indexPlayerCard;
    [SerializeField] private int indexEnemyCard;
    private bool isOnGoingBattle;

    [Header("Events")]
    public OnShowButtonEventSO OnShowButton;
    public OnHideButtonEventSO OnHideButton;
    public OnEnemyPickingCardEventSO onEnemyPickingCardEvent;
    public OnChangeEnemyEventSO onChangeEnemy;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        SetupCarDeckForPlayer();
    }

    #region SET UP DECK
    private void SetupCarDeckForPlayer()
    {
        maxSlots = playerBattleContainer.transform.childCount;
        playerBattleCardDecks = playerBattleContainer.GetComponentsInChildren<PlayerBattleCardDeck>();

        for (int i = 0; i < playerBattleCardDecks.Length; i++)
            playerBattleCardDecks[i].Init(i);

        playerBattleDecks = new CardInstance[maxSlots];
    }

    public void SetupCarDeckForEnemy(int slots)
    {
        EnemyBattleDecks = new CardInstance[slots];
    }

    private void SetEnemy(Enemy newEnemy)
    {
        currentEnemy = newEnemy;
    }

    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugEnemyBattleDeck();
            DebugPlayerBattleDeck();
        }
    }

    #region CHANGE DATA DECK
    public bool ChangeDataPlayerBattleDeck(CardInstance cardData, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= maxSlots)
            return false;

        playerBattleDecks[slotIndex] = cardData;

        if (playerBattleDecks.Length > 0)
        {
            OnShowButton.OnRaiseEvent(UIButtonContext.Battle, true);
        }

        //PlayerDeckManager.instance.HideItemCard();
        return true;
    }

    public bool ChangeDataEnemyBattleDeck(CardInstance cardData, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= maxSlots)
            return false;

        EnemyBattleDecks[slotIndex] = cardData;

        return true;
    }

    #endregion

    public void BattleStart()
    {
        //call enemy to stand by the battleDeck
        onEnemyPickingCardEvent.OnRaise(maxSlots);
    }

    public void ReadyForBattle()
    {
        Debug.Log("The Battle BEGGUN");

        OnGoingBattle();

        StartCoroutine(DelayDebugLog());
    }

    public void OnGoingBattle()
    {
        if (isOnGoingBattle) return;
        indexEnemyCard  = 0;
        indexPlayerCard = 0;

        isOnGoingBattle = true;
        StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine()
    {
        while (indexEnemyCard < EnemyBattleDecks.Length || indexPlayerCard < playerBattleDecks.Length)
        {
            CardInstance playerCard = indexPlayerCard < playerBattleDecks.Length ? playerBattleDecks[indexPlayerCard] : null;
            CardInstance enemyCard = indexEnemyCard < EnemyBattleDecks.Length ? EnemyBattleDecks[indexEnemyCard] : null;

            DeciderManager.instance.DecideCard(playerCard, enemyCard);

            indexPlayerCard++;
            indexEnemyCard++;

            yield return new WaitForSeconds(1f); // delay antar slot battle
        }

        EndBattle();
    }

    public void EndBattle()
    {
        for (int i = 0; i < playerBattleDecks.Length; i++)
        {
            playerBattleCardDecks[i].ResetPlayerBattleCardDeck();
            playerBattleDecks[i] = null;
        }

        SetupCarDeckForPlayer();
    }

    public void ResetOnGoingBattle() { isOnGoingBattle = false; }

    private void OnEnable()
    {
        onChangeEnemy.Register(SetEnemy);
    }

    private void OnDisable()
    {
        onChangeEnemy.Unregister(SetEnemy);
    }

    #region DEBUGGING AREA
    private void DebugPlayerBattleDeck()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            string cardName = "EMPTY";

            if (playerBattleDecks[i] != null && playerBattleDecks[i].cardData != null)
            {
                cardName = playerBattleDecks[i].cardData.cardName;
            }

            Debug.Log($"{i + 1}. {cardName}, Player Battle Card Deck {i + 1}");
        }
    }

    private void DebugEnemyBattleDeck()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            string cardName = "EMPTY";

            if (EnemyBattleDecks[i] != null && EnemyBattleDecks[i].cardData != null)
            {
                cardName = EnemyBattleDecks[i].cardData.cardName;
            }

            Debug.Log($"{i + 1}. {cardName}, Enemy Battle Card Deck {i + 1}");
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
