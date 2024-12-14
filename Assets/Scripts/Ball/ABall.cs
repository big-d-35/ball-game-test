using UnityEngine;

public abstract class ABall : ScriptableObject
{
    [SerializeField] private float strength;
    public float Strength => strength;

    [SerializeField] private float stopFactor;
    public float StopFactor => stopFactor;

    [SerializeField] private float stopTime;
    public float StopTime => stopTime;
}