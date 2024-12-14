using UnityEngine;

public class BallStopEvent : MonoBehaviour
{
    public delegate void StopEvent();
    public static event StopEvent OnStop;
    
    private void OnCollisionStay(Collision other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Ground") && GetComponent<Rigidbody>().linearVelocity.magnitude < 0.1f)
        {
            BallStopped();
        } else if (other.gameObject.CompareTag("OutOfBounds"))
        {
            BallStopped();
        }
    }
    
    private void BallStopped()
    {
        OnStop?.Invoke();
    }
}
