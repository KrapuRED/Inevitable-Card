using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;

    [Header("Battle Deck Animation")]
    public float EndScale;
    public float defaultScale;
    public float time;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayEnterCardClashAnimation(PlayerBattleCardDeck playerCard, EnemyBattleCardDeck enemyCard)
    {
        playerCard.ClashCardEnterAnimation(EndScale, time);
    }
    public void PlayExitCardClashAnimation(PlayerBattleCardDeck playerCard, EnemyBattleCardDeck enemyCard)
    {
        playerCard.ClasCardExitAnimation(defaultScale, time);
    }

    public void ApplyVisuaEffect()
    {

    }
}
