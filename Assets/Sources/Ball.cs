using UnityEditor.EditorTools;
using UnityEngine;

public interface IBall
{
    void Shoot();
}

public class Ball : MonoBehaviour, IBall
{
    [SerializeField] private Draw draw;
    private Rigidbody ballRigidBody;
    private float velosity = 2f;
    private Vector3 direction = new Vector3(0, 3.5f, -2.4f);
    private bool isShoot;

    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody>();
        draw.DrawTrajectory(!isShoot);

    }

    public void Shoot()
    {
        isShoot = true;
        draw.DrawTrajectory(!isShoot);
        ballRigidBody.linearVelocity = velosity * direction;
    }

}
