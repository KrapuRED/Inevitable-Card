using UnityEngine;

public class CurrentEnemyHandler : MonoBehaviour
{
    [Header("Events")]
    public OnChangeEnemyEventSO onChangeEnemy;

    public void SetCurrentEnemy(Enemy enemy)
    {
        onChangeEnemy.RaiseEvent(enemy);
        BattleManager.instance.BattleStart();
    }   
}
