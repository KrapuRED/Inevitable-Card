using System.Collections;
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

    public void ApplyVisuaEffect()
    {

    }
}
