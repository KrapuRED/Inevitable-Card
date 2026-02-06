using UnityEngine;

public class GuideButtonUI : CostumizeButton
{
    private void Start()
    {
        HUDManager.instance.SetPanelIsOpen();
    }

    public override void OnClickButton()
    {
        HUDManager.instance.OpenPanel(PanelName.GuidePanel);
    }
}
