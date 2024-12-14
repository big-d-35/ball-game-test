using UnityEngine;
using UniRx;
public class BallEvents
{
    public ReactiveCommand<Vector3> onBallPlaced { get; private set; } = new ReactiveCommand<Vector3>();
    public ReactiveCommand onGoal { get; private set; } = new ReactiveCommand();
    public ReactiveCommand<Vector3> onStopped { get; private set; } = new ReactiveCommand<Vector3>();
    public ReactiveCommand<Vector3> onPushed {get; private set; } = new ReactiveCommand<Vector3>();
}
