using System.Collections;
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

    [Header("State Battle Arena")]
    [SerializeField] private int turn;
    [SerializeField] private PlayerCharacter _currentPlayer;
    [SerializeField] private Enemy _currentEnemy;
    [SerializeField] private int _indexPlayerCard;
    [SerializeField] private int _indexEnemyCard;
    [SerializeField] private bool _isOnGoingBattle;
    private int _totalCostStamina;

    [Header("Events")]
    public OnShowButtonEventSO OnShowButton;
    public OnHideButtonEventSO OnHideButton;
    public OnEnemyPickingCardEventSO onEnemyPickingCardEvent;
    public OnChangeEnemyEventSO onChangeEnemy;
    public OnChangePlayerEventSO onChangePlayer;
    public OnResetCardDeckSO onResetCardDeck;

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

    private void SetPayer(PlayerCharacter newPlayer)
    {
        _currentPlayer = newPlayer;
    }

    #endregion

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugEnemyBattleDeck();
            DebugPlayerBattleDeck();
        }
    }*/

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
        Debug.Log("CheckPlayerDeck Get Called");
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
        OnShowButton.OnRaiseEvent(UIButtonContext.Battle, hasAnyCard);
    }

    public void CalculateStamina()
    {
        _totalCostStamina = 0;

        for (int i = 0; i < _playerBattleDecks.Length; ++i)
        {
            if (_playerBattleDecks[i] != null && _playerBattleDecks[i].cardData != null)
            {
                _totalCostStamina += _playerBattleDecks[i].cardData.StaminaCost;
            }
        }

        HUDManager.instance.UpdatePlayerStaminaPreview(_totalCostStamina);
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

    public void ReadyForBattle()
    {
        int newCurrentStamina = _currentPlayer.currentStamina - _totalCostStamina;

        if (newCurrentStamina < 0)
        {
            return;
        }

        Debug.Log("The Battle BEGGUN");
        OnShowButton.OnRaiseEvent(UIButtonContext.Battle, false);
        OnGoingBattle();

        HUDManager.instance.CommitPlayerStamina(newCurrentStamina);
        //StartCoroutine(DelayDebugLog());
    }

    public void OnGoingBattle()
    {
        if (_isOnGoingBattle) return;
        _indexEnemyCard  = 0;
        _indexPlayerCard = 0;

        _isOnGoingBattle = true;
        StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine()
    {
        while (_indexEnemyCard < _EnemyBattleDecks.Length || _indexPlayerCard < _playerBattleDecks.Length)
        {
            CardInstance playerCard = _indexPlayerCard < _playerBattleDecks.Length ? _playerBattleDecks[_indexPlayerCard] : null;
            CardInstance enemyCard = _indexEnemyCard < _EnemyBattleDecks.Length ? _EnemyBattleDecks[_indexEnemyCard] : null;

            DeciderManager.instance.DecideCard(playerCard, enemyCard);

            _indexPlayerCard++;
            _indexEnemyCard++;

            yield return new WaitForSeconds(1f); // delay antar slot battle
        }

        EndBattle();
    }

    public void EndBattle()
    {
        for (int i = 0; i < _playerBattleDecks.Length; i++)
        {
            _playerBattleCardDecks[i].ResetPlayerBattleCardDeck();
            _playerBattleDecks[i] = null;
        }

        _isOnGoingBattle = false;
        onResetCardDeck.OnRaise();
        SetupCarDeckForPlayer();
    }

    public void SelectWinner(string defeat)
    {
        if (defeat != "enemy")
        {
            Debug.Log("Player is WIN");
            _currentEnemy = null;
        }
        else
        {
            Debug.Log("Enemy is WIN");
        }
    }

    public void EyeOfTheSpoiler()
    {
        
    }

    public void ResetOnGoingBattle() { _isOnGoingBattle = false; }

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
