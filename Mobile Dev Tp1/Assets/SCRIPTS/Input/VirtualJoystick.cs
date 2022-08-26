using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform stick;
    [SerializeField] private Image Background;

    public string player = "";
    public float limit = 250f;

    private void OnDisable()
    {
        SetHorizontal(0);
        SetVertical(0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pos = ConverToLocal(eventData);
        if (pos.magnitude > limit)
            pos = pos.normalized * limit;
        stick.anchoredPosition = pos;

        var x = pos.x / limit;
        var y = pos.y / limit;

        SetHorizontal(x);
        SetVertical(y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Background.color = Color.red;
        stick.anchoredPosition = ConverToLocal(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Background.color = Color.gray;
        stick.anchoredPosition = Vector2.zero;
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