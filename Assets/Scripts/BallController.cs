using UnityEngine;

public class BallController : MonoBehaviour
{
    public Transform hoop;
    public float arcHeight = 5f;
    public float spinAmount = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void ThrowBall()
    {
        rb.isKinematic = false;
        Vector3 start = transform.position;
        Vector3 end = hoop.position;
        Vector3 velocity = CalculateVelocity(start, end, Random.Range(arcHeight-2, arcHeight+2));
        rb.linearVelocity = velocity;
        ApplySpin(velocity);
    }

    private Vector3 CalculateVelocity(Vector3 start, Vector3 end, float height)
    {
        float gravity = Mathf.Abs(Physics.gravity.y);
        float displacementY = end.y - start.y;
        Vector3 displacementXZ = new Vector3(end.x - start.x, 0, end.z - start.z);
        float time = Mathf.Sqrt(2 * height / gravity) + Mathf.Sqrt(2 * (height - displacementY) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / time;
        return velocityXZ + velocityY; 
    }

    private void ApplySpin(Vector3 velocity)
    {
        Vector3 spin = velocity.normalized * spinAmount;
        rb.AddTorque(spin);
    }
}
