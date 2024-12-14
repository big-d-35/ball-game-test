using UniRx;
using UnityEngine;

public class BallPushController : ABallPushController
{
    [SerializeField] private ABallCamera cameraForDragging;
    [SerializeField] private ABallController ballController;



    private Vector2 startScreenPosition;
    private Vector2 endScreenPosition;

    private bool isControlLocked = false;

    

    public override void StartDraggingBall(Vector2 position)
    {
        if(isControlLocked) return;

        startScreenPosition = position;
        endScreenPosition = position;
    }

    public override void DragBall(Vector2 position)
    {
        if(isControlLocked) return;

        endScreenPosition = position;
        cameraForDragging.IsDragging(endScreenPosition - startScreenPosition);
    }

    public override void PushBall()
    {
        if(isControlLocked) return;

        cameraForDragging.IsDragging(Vector2.zero);

        Vector3 screenDirection = endScreenPosition - startScreenPosition;

        Vector3 pushDirection = new Vector3(
            screenDirection.x,
            screenDirection.y,
            Vector3.Magnitude(screenDirection)
        );

        ballController.Push(pushDirection);

        isControlLocked = true;

        ballController.BallEvents.onBallPlaced.Take(1)
            .Subscribe(pos => 
            {
                isControlLocked = false;
            }).AddTo(this);
    }
}