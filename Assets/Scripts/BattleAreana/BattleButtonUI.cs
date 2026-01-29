using UnityEngine;

public class BattleButtonUI : MonoBehaviour
{
    public UIButtonContext buttonContext;

    [Header("Refences UI")]
    public GameObject InitiateMoveButton;

    [Header("Events")]
    public OnShowButtonEventSO OnShowButton;
    public OnHideButtonEventSO OnHideButton;

    public void ShowButton(UIButtonContext context, bool show)
    {
        InitiateMoveButton.SetActive(show);
    }

    public void HideButton(UIButtonContext context, bool show)
    {
        InitiateMoveButton.SetActive(show);
    }

    private void OnEnable()
    {
        OnShowButton.Register(ShowButton);
        OnHideButton.Register(HideButton);
    }

    private void OnDisable()
    {
        OnShowButton.Unregister(ShowButton);
        OnHideButton.Unregister(HideButton);
    }
}
