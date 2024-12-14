using Unity.VisualScripting;
using UnityEngine;

public abstract class ABallPushController : MonoBehaviour
{
    public abstract void StartDraggingBall(Vector2 position);
    public abstract void DragBall(Vector2 position);
    public abstract void PushBall();
}