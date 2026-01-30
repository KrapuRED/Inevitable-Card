using DG.Tweening;
using UnityEngine;

public class BattleCardDeck : Card
{

    public virtual void Init(int index)
    {

    }

    #region Receive Section
    public virtual void EnterReceiveZone()
    {
        
    }

    public virtual void ReceivePlayerCard(CardInstance card)
    {
        
    }

    public virtual void ExitReceiveZone()
    {
        
    }

    #endregion

    #region Hovering Secetion
    public  override void OnHoverEnter()
    {
        
    }

    public  override void OnClickCard()
    {
        
    }

    public  override void OnHoverExit()
    {
        
    }
    #endregion

    #region Dragging Section
    public  override void StartDragging(Vector2 mouseWorldPosition)
    {
        
    }

    public  override void Drag(Vector2 mouseWorldPosition)
    {
        
    }

    public  override void EndDrag(bool droppedSuccessfully)
    {
        
    }
    #endregion

    public virtual void ChangeStateBattleDeck()
    {
        
    }

    public virtual void CancelCard()
    {
        
    }

}
