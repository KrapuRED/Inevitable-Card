using UnityEngine;

public class GuideButtonUI : CostumizeButton
{
    public override void OnClickButton()
    {
        HUDManager.instance.OpenPanel(PanelName.GuidePanel);
    }
}
