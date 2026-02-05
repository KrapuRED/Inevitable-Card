using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Config Battle Arena")]
    private int maxSlots;
    public GameObject playerBattleContainer;
    private PlayerBattleCardDeck[] _playerBattleCardDecks;
    [SerializeField] private CardInstance[] _playerBattleDecks;
    [SerializeField] private CardInstance[] _EnemyBattleDecks;
    [SerializeField] private List<PlayerBattleCardDeck> playerBattleCardDecks;
    [SerializeField] private List<EnemyBattleCardDeck> enemyBattleCardDecks;

    [Header("State Battle Arena")]
    [SerializeField] private int turn;
    [SerializeField] private Player _currentPlayer;
    [SerializeField] private Enemy _currentEnemy;
    [SerializeField] private int _indexPlayerCard;
    [SerializeField] private int _indexEnemyCard;
    [SerializeField] private bool _isOnGoingBattle;
    public bool IsBattleOngoing => _isOnGoingBattle;
    private int _totalCostStamina;

    [Header("Events")]
    public OnShowButtonEventSO OnShowButton;
    public OnHideButtonEventSO OnHideButton;
    public OnEnemyPickingCardEventSO onEnemyPickingCardEvent;
    public OnChangeEnemyEventSO onChangeEnemy;
    public OnChangePlayerEventSO onChangePlayer;
    public OnResetCardDeckSO onResetCardDeck;
    public OnEyeOfTheSpoilerEventSO onEyeOfTheSpoilerEvent;
    public OnSetEnemyCardDeckEventSO onChangeEnemyCardDeck;

    [SerializeField] private Coroutine _battleCoroutine;

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
        if (_currentPlayer != null)
            _currentPlayer.ResetStamina();

        maxSlots = playerBattleContainer.transform.childCount;
        _playerBattleCardDecks = playerBattleContainer.GetComponentsInChildren<PlayerBattleCardDeck>();

        for (int i = 0; i < _playerBattleCardDecks.Length; i++)
            _playerBattleCardDecks[i].Init(i);

        _playerBattleDecks = new CardInstance[maxSlots];
    }

    public void SetupCarDeckForEnemy(int slots)
    {
        _EnemyBattleDecks = new CardInstance[slots];
    }

    private void SetEnemy(Enemy newEnemy)
    {
        _currentEnemy = newEnemy;
    }

    private void SetPayer(Player newPlayer)
    {
        _currentPlayer = newPlayer;
    }

    #endregion

    #region CHANGE DATA DECK
    public bool ChangeDataPlayerBattleDeck(CardInstance cardData, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= maxSlots)
            return false;

        _playerBattleDecks[slotIndex] = cardData;

        CheckPlayerDeck();
        CalculateStamina();

        //PlayerDeckManager.instance.HideItemCard();
        return true;
    }

    public void CheckPlayerDeck()
    {
        //Debug.Log("CheckPlayerDeck Get Called");
        bool hasAnyCard = false;

        for (int i = 0; i < _playerBattleDecks.Length; ++i)
        {
            if (_playerBattleDecks[i] != null && _playerBattleDecks[i].cardData != null)
            {
                hasAnyCard = true;
                break; // stop early, we already found one
            }
        }

        Debug.Log("hasAny Card : " + hasAnyCard);
        HUDManager.instance.ShowButton(UIButtonContext.Battle, hasAnyCard);
        //OnShowButton.OnRaiseEvent(UIButtonContext.Battle, hasAnyCard);
    }

    public void CalculateStamina()
    {
        _totalCostStamina = 0;

        for (int i = 0; i < _playerBattleDecks.Length; ++i)
        {
            if (_playerBattleDecks[i] != null && _playerBattleDecks[i].cardData != null)
            {
                _totalCostStamina += _playerBattleDecks[i].cardData.staminaCost;
            }
        }

        HUDManager.instance.UpdatePlayerStaminaPreview(_currentPlayer.currentStamina, _totalCostStamina);
    }

    public bool ChangeDataEnemyBattleDeck(CardInstance cardData, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= maxSlots)
            return false;

        _EnemyBattleDecks[slotIndex] = cardData;

        return true;
    }

    #endregion

    public void BattleStart()
    {
        //call enemy to stand by the battleDeck
        onEnemyPickingCardEvent.OnRaise(maxSlots);
    }

    public void RefreshEnemyDeckPhase()
    {
        // 1. Stop battle & clear everything
        CancelBattle(false);

        if (_currentEnemy == null)
        {
            Debug.Log("_currentEnemy is empty");
            return;
        }

        EnemyPickCard enemyPick = _currentEnemy.GetComponentInChildren<EnemyPickCard>();
        if (enemyPick == null)
        {
            Debug.Log("enemyPick is empty");
            return;
        }

        // 2. Generate SEQUENTIAL boss deck
        List<CardInstance> enemyCards = enemyPick.GetEnemyCardListSequential(maxSlots);

        for (int i = 0; i < enemyCards.Count; i++)
            Debug.Log($"{i}. {enemyCards[i].cardData.cardName}");

        _EnemyBattleDecks = enemyCards.ToArray();

        // 3. No hidden cards in phase 2
        int hiddenCard = 0;

        // 4. Build UI
        onChangeEnemyCardDeck.OnSetEnemyDeck(hiddenCard, _EnemyBattleDecks);

        // 5. Force reveal (safety)
        EyeOfTheSpoiler();
    }

    public void ReadyForBattle()
    {
        int newCurrentStamina = _currentPlayer.currentStamina - _totalCostStamina;

        if (newCurrentStamina < 0)
        {
            return;
        }

        for (int i = 0; i < _playerBattleCardDecks.Length; i++)
        {
            _playerBattleCardDecks[i].SetCannotChangeCard();
        }

        Debug.Log("The Battle BEGGUN");
        HUDManager.instance.HideButton(UIButtonContext.Battle, false);
        OnGoingBattle();

        HUDManager.instance.CommitPlayerStamina(newCurrentStamina);
        //StartCoroutine(DelayDebugLog());
    }

    private void OnGoingBattle()
    {
        if (_isOnGoingBattle) return;
        _indexEnemyCard  = 0;
        _indexPlayerCard = 0;

        EyeOfTheSpoiler();

        _isOnGoingBattle = true;
        _battleCoroutine = StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine()
    {
        while (_battleCoroutine != null && _indexEnemyCard < _EnemyBattleDecks.Length || _indexPlayerCard < _playerBattleDecks.Length)
        {
            if (_isOnGoingBattle)
            {
                CardInstance playerCard = _indexPlayerCard < _playerBattleDecks.Length ? _playerBattleDecks[_indexPlayerCard] : null;
                CardInstance enemyCard = _indexEnemyCard < _EnemyBattleDecks.Length ? _EnemyBattleDecks[_indexEnemyCard] : null;

                PlayerBattleCardDeck playerDeck =
                    _indexPlayerCard < playerBattleCardDecks.Count ? playerBattleCardDecks[_indexPlayerCard] : null;

                EnemyBattleCardDeck enemyDeck =
                    _indexEnemyCard < enemyBattleCardDecks.Count ? enemyBattleCardDecks[_indexEnemyCard] : null;

                yield return StartCoroutine(AnimationManager.instance.PlayEnterCardClashAnimation(playerDeck, enemyDeck));

                DeciderManager.instance.DecideCard(playerCard, enemyCard, _currentPlayer.baseDamage, _currentEnemy.baseDamage);

                yield return StartCoroutine(AnimationManager.instance.PlayExitCardClashAnimation(playerDeck, enemyDeck));

                _indexPlayerCard++;
                _indexEnemyCard++;
            }

            yield return new WaitForSeconds(1f);
        }

        if (_isOnGoingBattle)
            EndBattle();
    }

    public void EndBattle()
    {
        ResetDeckCard();

        _isOnGoingBattle = false;

        SetupCarDeckForPlayer();
    }

    private void CancelBattle(bool resetDeck = true)
    {
        if (!_isOnGoingBattle) return;

        if (_battleCoroutine != null)
        {
            StopCoroutine(_battleCoroutine);
            _battleCoroutine = null;
        }

        _isOnGoingBattle = false;

        for (int i = 0; i < maxSlots; i++)
        {
            _playerBattleCardDecks[i].ResetPlayerBattleCardDeck();
            _playerBattleDecks[i] = null;
        }

        if (resetDeck)
            EndBattle();
    }

    public void SelectWinner(TargetType defeat)
    {
        if (defeat == TargetType.Enemy)
        {
            Debug.Log("Player is WIN");
            if (_currentEnemy.enemyData.enemyType == EnemyType.Goon)
            {
                GameManager.instance.PlayerWin();
                
                if (_currentEnemy.enemyData.enemyType == EnemyType.Goon)
                    GachaManager.instance.GachaCard(_currentEnemy.enemyData.enemyStatus);

                _currentEnemy = null;
            }
        }
        else
        {
            Debug.Log("Enemy is WIN");
            GameManager.instance.PlayerLose();
        }

        CancelBattle();
    }

    public void EyeOfTheSpoiler()
    {
        onEyeOfTheSpoilerEvent.RaiseEvent();
    }

    private void ResetDeckCard()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            _playerBattleCardDecks[i].ResetPlayerBattleCardDeck();
            _playerBattleDecks[i] = null;
        }

        onResetCardDeck.OnRaise();
    }
         

    private void OnEnable()
    {
        onChangeEnemy.Register(SetEnemy);
        onChangePlayer.Register(SetPayer);
    }

    private void OnDisable()
    {
        onChangeEnemy.Unregister(SetEnemy);
        onChangePlayer.Unregister(SetPayer);
    }

    #region DEBUGGING AREA
    private void DebugPlayerBattleDeck()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            string cardName = "EMPTY";

            if (_playerBattleDecks[i] != null && _playerBattleDecks[i].cardData != null)
            {
                cardName = _playerBattleDecks[i].cardData.cardName;
            }

            Debug.Log($"{i + 1}. {cardName}, Player Battle Card Deck {i + 1}");
        }
    }

    private void DebugEnemyBattleDeck()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            string cardName = "EMPTY";

            if (_EnemyBattleDecks[i] != null && _EnemyBattleDecks[i].cardData != null)
            {
                cardName = _EnemyBattleDecks[i].cardData.cardName;
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
