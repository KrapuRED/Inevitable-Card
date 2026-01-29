using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeckController : MonoBehaviour
{
    public EnemySO enemyData;
    [Header("Refence Enemy Scripts")]
    public EnemyPickCard enemyPickCard;
    [SerializeField] private CardInstance[] enemyCards;

    [Header("Events")]
    public OnSetEnemyCardDeckEventSO OnSetEnemyCardDeckEvent;
    public OnEnemyPickingCardEventSO OnSetEnemyPickingCardEvent;

    public void PickCard(int slots)
    {
        enemyCards = enemyPickCard.GetEnemyCardList(slots).ToArray();
        StartCoroutine(DelaySendData());
    }

    IEnumerator DelaySendData()
    {
        yield return new WaitForSeconds(0.5f);
        
        OnSetEnemyCardDeckEvent.OnRaiseEvent(enemyData.hiddenCard, enemyCards);
    }

    private void OnEnable()
    {
        OnSetEnemyPickingCardEvent.Register(PickCard);
    }

    private void OnDisable()
    {
        OnSetEnemyPickingCardEvent.Unregister(PickCard);
    }
}
