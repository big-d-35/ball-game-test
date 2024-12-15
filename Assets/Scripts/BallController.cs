using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{

    [SerializeField] private GameObject Arrow;
    [SerializeField] private Transform Basket;

    [SerializeField, Range(1, 10)] private int _minForce;
    [SerializeField, Range(1, 5)] private float MaxHoldTime;

    private Vector3 startForceScale;
    private Vector3 startLocalDirection;
    private Vector3 startPosition;
    private Rigidbody RB;
    private float _force;
    private float timer;
    private bool increaseForce;
    private bool OnTheGround;
    private void Start()
    {
        startForceScale = Arrow.transform.localScale;
        RB = GetComponent<Rigidbody>();
        InputData._startAiming += AimingStart;
        InputData._disableAiming += DisableAimin;
        InputData._positing += Aiming;
    }


    /// <summary>
    /// Момент нажатия стика
    /// </summary>
    private void AimingStart()
    {
        transform.LookAt(Basket);
        Arrow.SetActive(value: true);
        Arrow.transform.LookAt(Basket);
        startPosition = transform.position;
        startLocalDirection = Arrow.transform.localEulerAngles;
        increaseForce = true;
        _force = _minForce;
    }


    /// <summary>
    /// Момент отпускания стика
    /// </summary>
    private void DisableAimin()
    {
        Arrow.transform.localScale = startForceScale;
        RB.useGravity = true;
        RB.AddForce(Arrow.transform.forward.normalized * _force, ForceMode.Impulse);
        increaseForce = false;
        Arrow.SetActive(false);
        InputData.SwitchCam(true);
        timer = 0;
    }

    /// <summary>
    /// Прицеливание
    /// </summary>
    /// <param name="delta"></param>
    private void Aiming(Vector2 delta)
    {
        Arrow.transform.localEulerAngles = new Vector3(startLocalDirection.x + delta.y * -30, startLocalDirection.y + delta.x * 30, startLocalDirection.z);
    }

    private void FixedUpdate()
    {
        if(increaseForce)
        {
            timer += Time.deltaTime;
            if (timer < MaxHoldTime)
            {
                float dif = Time.deltaTime;
                _force += dif * 2;
                Arrow.transform.localScale = new Vector3(Arrow.transform.localScale.x + dif, Arrow.transform.localScale.y + dif, Arrow.transform.localScale.z + dif);
            }
        }

        if (RB.linearVelocity.magnitude < 0.1f && OnTheGround)
        {
            ResetBall();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
            OnTheGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
            OnTheGround = false;
    }

    public void ResetBall()
    {
        RB.useGravity = false;
        RB.angularVelocity = new Vector3(0, 0, 0);
        RB.linearVelocity = new Vector3(0, 0, 0);
        transform.position = startPosition;
        InputData.SwitchCam(false);
    }
}
