using UnityEngine;

public class InitiateMoveButtonUI : CostumizeButton
{
    public override void OnClickButton()
    {
        BattleManager.instance.ReadyForBattle();
    }
}
