using UnityEngine;

public class PanelButton : CostumizeButton
{
    public PanelName panelName;

    public override void OnClickButton()
    {
        if (HUDManager.instance.IsPanelOpened)
            HUDManager.instance.ClosePanel();
        else
            HUDManager.instance.OpenPanel(panelName);

        SoundEffectManager.instance.PlaySoundEffectOneClip("ClickButton");
    }
}
