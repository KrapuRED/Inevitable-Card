using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum StatusType
{
    Health,
    Stamina
}

[System.Serializable]
public enum PanelName
{
    EyeOfTheSpoilerPanel,
    GameOverPanel,
    WinningPanel
}

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [Header("Reference UI Panel")]
    public GameObject eyeOfTheSpoilerPanel;
    public GameObject gameOverPanel;
    public GameObject WinningPanel;

    [Header("Reference UI")]
    public GameObject InitiateMoveButton;
    public GameObject statusPlayer;
    public GameObject statusEnemy;

    [Header("Reference Script")]
    public HUDBorderCard playerCard;
    public HUDBorderCard enemyCard;
    public ObtainCardControllerUI obtainCardController;

    [SerializeField]private bool isPanelOpened;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    #region Player and Enemy Status
    // Called when selecting/deselecting cardDatas
    public void UpdatePlayerStaminaPreview(int usedStamina)
    {
        playerCard.SetUsedStamina(usedStamina);
    }

    // Called AFTER pressing Battle button (stamina actually spent)
    public void CommitPlayerStamina(int newCurrentStamina)
    {
        playerCard.SetStamina(newCurrentStamina, 0);
        playerCard.SetUsedStamina(0);
    }

    public void UpdatePlayerHealth(float currentHealtPoint, float maxHealtPoint)
    {
        playerCard.SetHealth(currentHealtPoint, maxHealtPoint);
    }

    public void UpdateEnemyHealth(float currentHealtPoint, float maxHealtPoint)
    {
        enemyCard.SetHealth(currentHealtPoint, maxHealtPoint);
    }
    #endregion

    #region Panel Section
    public void OpenPanel(PanelName panelName)
    {
        switch (panelName)
        {
            case PanelName.EyeOfTheSpoilerPanel:
                eyeOfTheSpoilerPanel.SetActive(true);
                isPanelOpened = true;
                break;

            case PanelName.GameOverPanel:
                gameOverPanel.SetActive(true);
                isPanelOpened = true;
                break;

            case PanelName.WinningPanel:
                WinningPanel.SetActive(true);
                isPanelOpened = true;
                break;

            default:
                isPanelOpened = false;
                break;
        }
    }

    public void ClosePanel()
    {
        if (isPanelOpened)
        {
            eyeOfTheSpoilerPanel.SetActive(false);
            gameOverPanel.SetActive(false);
            WinningPanel.SetActive(false);
            isPanelOpened = false;
        }
    }
    #endregion

    #region Button Section
    public void ShowButton(UIButtonContext context, bool show)
    {
        InitiateMoveButton.SetActive(show);
    }

    public void HideButton(UIButtonContext context, bool show)
    {
        InitiateMoveButton.SetActive(show);
    }
    #endregion

    public void UpdateObtainCard(List<CardSO> obtainCard)
    {
        obtainCardController.SetObtainCards(obtainCard);
    }

    public bool IsPanelOpened { get { return isPanelOpened; } }
}
