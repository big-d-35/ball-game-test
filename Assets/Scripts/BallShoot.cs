using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShoot : MonoBehaviour
{
    public Transform spawnTransform;
    public Transform targetTransform;

    public float AngleInDegrees;

    public GameObject Ball;

    private float g = Physics.gravity.y;

    private bool isPressed;

    private void Start()
    {
        //a bit change target position
        targetTransform.position = new Vector3(targetTransform.position.x + Random.Range(-0.15f, 0.15f), targetTransform.position.y + Random.Range(-0.15f, 0.15f), targetTransform.position.z + Random.Range(-0.15f, 0.15f));
        isPressed = false;
    }


    private void Update()
    {
        //spawner angle
        spawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            if (!isPressed)
            {
                Shot();
                isPressed = true;
            }
        }
    }

    public void Shot()
    {
        Rigidbody BallRb = Ball.GetComponent<Rigidbody>();
        BallRb.useGravity = true;

        //look at the ring
        Vector3 fromTo = targetTransform.position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        //magnitude of a vector from the ball to the ring
        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        BallRb.velocity = spawnTransform.forward * v;
    }
}
