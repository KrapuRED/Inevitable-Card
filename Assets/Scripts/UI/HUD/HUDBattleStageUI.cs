using UnityEngine;
using UnityEngine.UI;

public class HUDBattleStageUI : MonoBehaviour
{
    [SerializeField] private Image[] battleStageIcon;
    [SerializeField] private Sprite enemyElimination;
    [SerializeField] private Sprite enemyCurrent;
    [SerializeField] private Sprite enemyBossCurrent;


    public void NextBattleStageUI(int indexIcon)
    {
        if (indexIcon < 4)
        {
            battleStageIcon[indexIcon].sprite = enemyCurrent;
        }
        else
        {
            battleStageIcon[indexIcon].sprite = enemyBossCurrent;
        }
        ChangeBattleStageUI(indexIcon - 1);
    }

    private void ChangeBattleStageUI(int indexIcon)
    {
        battleStageIcon[indexIcon].sprite = enemyElimination;
    }
}
