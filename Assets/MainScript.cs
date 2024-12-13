using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class MainScript : MonoBehaviour
{
    [SerializeField]private GameObject _ball;
    [SerializeField]private SplineContainer _spline;
    [SerializeField]private float _force = 10f;
    private Rigidbody ballRigidbody;
    private float lengthSpline;
    private float speed;
    private float progress;
    private bool isMoving = false;
    private float decelerationAmount = 2f;
    private Vector3 ballPosition = new Vector3(-0.793f, -3.5026f, -6.63f);
    private bool isBtn;
    private bool isMagnitude = false;

    private void Start()
    {
        lengthSpline = _spline.CalculateLength();
        ballRigidbody = _ball.GetComponent<Rigidbody>();
        StartThrow();
        isBtn = true;
    }
    private void Update()
    {
        if (isMoving)
        {
            progress += speed * Time.deltaTime / lengthSpline;
            progress = Mathf.Clamp01(progress);

            Vector3 position = _spline.EvaluatePosition(progress);
            _ball.transform.position = position;

            float3 forward = _spline.EvaluateTangent(progress);
            forward = math.normalize(forward);

            speed -= decelerationAmount * Time.deltaTime;

            if (progress >= 1f)
            {
                isMoving = false;
                ballRigidbody.isKinematic = false;
                ballRigidbody.linearVelocity = forward * Mathf.Max(speed, 0.1f);
                isMagnitude = true;
            }
        }
        if(isMagnitude)
        {
            if (!isMoving && ballRigidbody != null && ballRigidbody.linearVelocity.magnitude > 0.1f)
            {
                ballRigidbody.linearDamping = 0.7f;
                ballRigidbody.angularDamping = 0.7f;

                if (Mathf.Abs(ballRigidbody.linearVelocity.magnitude) < 0.2f)
                {
                    ballRigidbody.linearVelocity = Vector3.zero;
                    isBtn = true;
                    isMoving = false;
                    StartThrow();
                }
            }
        }
        
    }
    public void StartThrow()
    {
        ballRigidbody.isKinematic = true;
        _ball.transform.position = ballPosition;
        speed = _force;
        progress = 0f;
    }
    public void OnClickButton()
    {
        if(isBtn == true)
        {
            isBtn = false;
            isMoving = true;
        }
    }
}
