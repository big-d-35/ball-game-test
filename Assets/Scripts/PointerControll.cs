using UnityEngine;
using UnityEngine.EventSystems;

public class PointerControll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InputData.StartAiming();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputData.DisableAiming();
    }
}
