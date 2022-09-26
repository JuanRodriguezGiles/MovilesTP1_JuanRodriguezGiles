using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialInput : MonoBehaviour,IDragHandler, IPointerUpHandler
{
    public string player = "";
    
    public void OnDrag(PointerEventData eventData)
    {
        var pos = ConverToLocal(eventData);
        
        SetHorizontal(pos.x);
        SetVertical(pos.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetHorizontal(0);
        SetVertical(0);
    }
    
    private void SetHorizontal(float val)
    {
        InputManager.Instance.SetAxis("Horizontal" + player, val);
    }

    private void SetVertical(float val)
    {
        InputManager.Instance.SetAxis("Vertical" + player, val);
    }
    
    private Vector2 ConverToLocal(PointerEventData eventData)
    {
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out newPos);
        return newPos;
    }
}