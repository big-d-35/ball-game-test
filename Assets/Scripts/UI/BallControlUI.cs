using UnityEngine;
using UnityEngine.EventSystems;

public class BallControlUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] ABallPushController controller;

    private bool isDragging = false;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        controller.StartDraggingBall(Input.mousePosition);
        isDragging = true;
    }

    void Update()
    {
        if(!isDragging) return;
        controller.DragBall(Input.mousePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        controller.PushBall();
    }
}