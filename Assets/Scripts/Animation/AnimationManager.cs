using System.Collections;
using UnityEngine;

public enum AnimationEffectType
{
    None,
    LightBlast,
    Heal,
    HeavyBlast,
    Parry,
    Dodge,
    Guard,
    Explosion
}

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;

    [Header("Character Animation")]
    [SerializeField] private Enemy _currentEnemy;
    [SerializeField] private Player _currentPlayer;

    [Header("Battle Deck Animation")]
    public float EndScale;
    public float defaultScale;
    public float time;

    [Header("Events")]
    public OnChangeEnemyEventSO onChangeEnemyEvent;
    public OnChangePlayerEventSO onChangePlayerEvent;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else
            Destroy(gameObject);
    }

    public void DoPlayerAnimation(AnimationEffectType effectType)
    {
        _currentPlayer.playerAnimation.PlayAnimation(effectType);
    }

    public void DoEnemyAnimation(AnimationEffectType effectType)
    {
        _currentEnemy.enemyAnimation.PlayAnimation(effectType);
    }

    public IEnumerator PlayEnterCardClashAnimation(PlayerBattleCardDeck playerCard, EnemyBattleCardDeck enemyCard)
    {
        playerCard.ClashCardEnterAnimation(EndScale, time);
        enemyCard.ClashCardEnterAnimation(EndScale, time);
        yield return new WaitForSeconds(time);
    }
    public IEnumerator PlayExitCardClashAnimation(PlayerBattleCardDeck playerCard, EnemyBattleCardDeck enemyCard)
    {
        playerCard.ClasCardExitAnimation(defaultScale, time);
        enemyCard.ClasCardExitAnimation(defaultScale, time);
        yield return new WaitForSeconds(time);
    }

    private void SetEnemy(Enemy newEnemy)
    {
        _currentEnemy = newEnemy;
    }

    private void SetPayer(Player newPlayer)
    {
        _currentPlayer = newPlayer;
    }


    private void OnEnable()
    {
        onChangeEnemyEvent.Register(SetEnemy);
        onChangePlayerEvent.Register(SetPayer);
    }

    private void OnDisable()
    {
        onChangeEnemyEvent.Unregister(SetEnemy);
        onChangePlayerEvent.Unregister(SetPayer);
    }
}
