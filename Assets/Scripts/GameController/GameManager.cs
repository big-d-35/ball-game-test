using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ABallController ballController;
    [SerializeField] Transform spawnPoint;

    void Start()
    {
        ballController.Place(spawnPoint.position);

        ballController.BallEvents.onStopped.Subscribe(Pose =>
        {
            ballController.Place(spawnPoint.position);
        }).AddTo(this);
    }
}