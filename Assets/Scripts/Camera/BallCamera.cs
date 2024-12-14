using UnityEngine;

public class BallCamera : ABallCamera
{
    [SerializeField] private float DistanceModifier;
    [SerializeField] private float LerpSpeed;

    private Vector3 StartPosition;
    private float distance = 0f;

    public override void IsDragging(Vector2 direction)
    {
        distance = direction.magnitude * DistanceModifier;
    }

    void Start()
    {
        StartPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            StartPosition + Vector3.back * distance,
            Time.deltaTime * LerpSpeed
        );
    }


}