using UnityEngine;

public class EyeOfTheSpoilerButtton : CostumizeButton
{
    public int costHealth;

    public override void OnClickButton()
    {
        BattleManager.instance.EyeOfTheSpoiler();
        DamageManager.instance.DealDamageToTarget(TargetType.Player, costHealth);
        SoundEffectManager.instance.PlaySoundEffectOneClip("TextButton");
        HUDManager.instance.ClosePanel();
    }
}
