using UnityEngine;

public class EyeOfTheSpoilerButtton : CostumizeButton
{
    public override void OnClickButton()
    {
        BattleManager.instance.EyeOfTheSpoiler();
    }
}
