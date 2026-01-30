using UnityEngine;
using System.Collections.Generic;

public class BattleDeckUIController : MonoBehaviour
{
    [SerializeField] private int maxSlots;
    public GameObject EnemyBattleDeck;
    [SerializeField] private EnemyBattleCardDeck[] enemyBattleCardDecks;

    [Header("Events")]
    public OnSetEnemyCardDeckEventSO OnSetEnemyCardDeckEvent;

    private void Start()
    {
        maxSlots = EnemyBattleDeck.transform.childCount;
        enemyBattleCardDecks = EnemyBattleDeck.GetComponentsInChildren<EnemyBattleCardDeck>();

        for (int i = 0; i < enemyBattleCardDecks.Length; i++)
            enemyBattleCardDecks[i].Init(i);

        BattleManager.instance.SetupCarDeckForEnemy(maxSlots);
    }

    public void SetCardDeck(int hiddenCard, CardInstance[] cards)
    {
        Debug.Log("get called");

        if (cards == null || cards.Length == 0) return;
        if (enemyBattleCardDecks == null || enemyBattleCardDecks.Length == 0) return;

        List<CardInstance> workingCards = new List<CardInstance>(cards);

        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < enemyBattleCardDecks.Length; i++)
            availableIndexes.Add(i);

        int hiddenToPlace = Mathf.Min(hiddenCard, availableIndexes.Count, workingCards.Count);

        //1. Set Hiden Enemy Card and set data then change the visual
        for (int i = 0; i < hiddenToPlace; i++)
        {
            int randSlotIndex = Random.Range(0, availableIndexes.Count);
            int deckIndex = availableIndexes[randSlotIndex];
            availableIndexes.RemoveAt(randSlotIndex);

            enemyBattleCardDecks[deckIndex].SetHiddenCard();

            int randCardIndex = Random.Range(0, workingCards.Count);
            CardInstance cardHidden = workingCards[randCardIndex];
            workingCards.RemoveAt(randCardIndex);

            enemyBattleCardDecks[deckIndex].ReceivePlayerCard(cardHidden);
        }

        //2. Set the remaning battle cardHidden deck with cards left
        foreach (int deckIndex in availableIndexes)
        {
            if (workingCards.Count == 0) break;

            CardInstance card = workingCards[0];
            workingCards.RemoveAt(0);

            enemyBattleCardDecks[deckIndex].ReceivePlayerCard(card);
        }


    }

    private void OnEnable()
    {
        OnSetEnemyCardDeckEvent.Register(SetCardDeck);
    }

    private void OnDisable()
    {
        OnSetEnemyCardDeckEvent.Unregister(SetCardDeck);
    }
}
