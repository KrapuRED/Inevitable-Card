using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{

    public virtual void UpdateBoxCollider2D(float visibleWidth)
    {
        
    }

    public virtual void InitializerCard(CardInstance newCardSO)
    {
       
    }

    public virtual void UseItem()
    {

    }

    #region Hovering Secetion
    public virtual void OnHoverEnter()
    {
        
    }

    public virtual void OnClickCard()
    {
       
    }

    public virtual void OnHoverExit()
    {
       
    }
    #endregion

    #region Dragging Section
    public virtual void StartDragging(Vector2 mouseWorldPosition)
    {
        
    }

    public virtual void Drag(Vector2 mouseWorldPosition)
    {
       
    }

    //if collide with card deck collider the data get copy to card deck 

    public virtual void EndDrag(bool droppedSuccessfully)
    {
        
    }
    #endregion
}
