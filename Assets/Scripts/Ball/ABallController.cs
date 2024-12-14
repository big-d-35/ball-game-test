using UnityEngine;

public abstract class ABallController : MonoBehaviour
{
    public abstract ABall Ball { get; }
    public BallEvents BallEvents = new BallEvents();

    public abstract void Push(Vector3 direction);
    public abstract void Place(Vector3 position);

}