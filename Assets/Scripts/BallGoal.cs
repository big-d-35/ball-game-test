using UnityEngine;

public class BallGoal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ScoreCounter.Instance.AddScore(3); 
    }
}
