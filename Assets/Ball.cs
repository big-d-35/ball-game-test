using UnityEngine;
using UnityEngine.SceneManagement;

public class BasketballController : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public GameObject arrowObject;
    public float maxForce;
    public float minArrowScale;
    public float maxArrowScale;
    public float rotationSpeed;

    public Color weakShotColor = new Color(0.4f, 1f, 0.4f);
    public Color mediumShotColor = new Color(1f, 0.8f, 0.2f);
    public Color strongShotColor = new Color(1f, 0.2f, 0.2f);

    private enum ShootState
    {
        Idle,
        Aiming,
        Charging
    }

    private ShootState currentState = ShootState.Idle;
    private Vector3 aimStartPoint;
    private Vector3 chargeStartPoint;
    private Camera mainCamera;
    private Material arrowMaterial;
    private float currentForce;
    private Vector3 shootDirection;
    private Renderer[] childRenderers; // Массив для хранения всех рендереров дочерних объектов

    void Start()
    {
        if (ballRigidbody == null)
            ballRigidbody = GetComponent<Rigidbody>();

        mainCamera = Camera.main;

        ballRigidbody.drag = 0.5f;
        ballRigidbody.angularDrag = 0.5f;

        if (arrowObject != null)
        {
            // Получаем все рендереры, включая дочерние
            childRenderers = arrowObject.GetComponentsInChildren<Renderer>();
            // Основной материал берём от первого рендерера
            arrowMaterial = childRenderers[0].material;
        }
    }

    void Update()
    {
        HandleInput();
        CheckBallPosition();

        if (currentState == ShootState.Aiming)
        {
            HandleRotation();
        }
    }

    void HandleRotation()
    {
        Vector3 currentRotation = arrowObject.transform.eulerAngles;

        if (Input.GetKey(KeyCode.A))
            currentRotation.y -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            currentRotation.y += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
            currentRotation.x -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            currentRotation.x += rotationSpeed * Time.deltaTime;

        arrowObject.transform.eulerAngles = currentRotation;
        shootDirection = arrowObject.transform.forward.normalized;
    }

    void HandleInput()
    {
        switch (currentState)
        {
            case ShootState.Idle:
                HandleIdleInput();
                break;
            case ShootState.Aiming:
                HandleAimingInput();
                break;
            case ShootState.Charging:
                HandleChargingInput();
                break;
        }
    }

    void HandleIdleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                StartAiming(hit.point);
            }
        }
    }

    void HandleAimingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCharging();
        }
    }

    void HandleChargingInput()
    {
        if (Input.GetMouseButton(0))
        {
            UpdateCharging();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ShootBall();
        }
    }

    void StartAiming(Vector3 hitPoint)
    {
        currentState = ShootState.Aiming;
        aimStartPoint = transform.position;
        arrowObject.SetActive(true);
        arrowObject.transform.position = aimStartPoint + new Vector3(0, 0, 0f);

        // Устанавливаем начальный масштаб (только по Z)
        Vector3 initialScale = arrowObject.transform.localScale;
        initialScale.x = minArrowScale;
        initialScale.y = minArrowScale;
        initialScale.z = minArrowScale;
        arrowObject.transform.localScale = initialScale;

        // Устанавливаем начальный цвет для всех дочерних объектов
        UpdateArrowColor(weakShotColor);
    }

    void StartCharging()
    {
        currentState = ShootState.Charging;
        chargeStartPoint = Input.mousePosition;
        currentForce = 0f;
    }

    void UpdateCharging()
    {
        float dragDistance = Vector3.Distance(Input.mousePosition, chargeStartPoint);
        currentForce = Mathf.Clamp01(dragDistance / 300f);

        // Изменяем размер стрелки только по оси Z
        Vector3 currentArrowScale = arrowObject.transform.localScale;
        currentArrowScale.x = minArrowScale;
        currentArrowScale.y = minArrowScale;
        currentArrowScale.z = Mathf.Lerp(minArrowScale, maxArrowScale, currentForce);
        arrowObject.transform.localScale = currentArrowScale;

        // Определяем текущий цвет на основе силы
        Color currentColor;
        if (currentForce < 0.3f)
        {
            currentColor = Color.Lerp(weakShotColor, mediumShotColor, currentForce / 0.3f);
        }
        else if (currentForce < 0.7f)
        {
            currentColor = Color.Lerp(mediumShotColor, strongShotColor, (currentForce - 0.3f) / 0.4f);
        }
        else
        {
            currentColor = strongShotColor;
        }

        // Обновляем цвет всех дочерних объектов
        UpdateArrowColor(currentColor);
    }

    // Новый метод для обновления цвета всех дочерних объектов
    void UpdateArrowColor(Color newColor)
    {
        if (childRenderers != null)
        {
            foreach (Renderer renderer in childRenderers)
            {
                renderer.material.color = newColor;
            }
        }
    }

    void ShootBall()
    {
        Vector3 finalDirection = shootDirection;
        finalDirection.y += 0.5f;
        finalDirection = finalDirection.normalized;

        float forcePower = currentForce * maxForce;

        Debug.Log($"Направление броска: {finalDirection}");
        Debug.Log($"Сила броска: {forcePower:F2} Н");
        Debug.Log($"Процент от максимальной силы: {(currentForce * 100):F1}%");

        ballRigidbody.isKinematic = false;
        ballRigidbody.velocity = Vector3.zero;

        ballRigidbody.AddForce(finalDirection * forcePower, ForceMode.Impulse);
        ballRigidbody.AddTorque(Random.insideUnitSphere * forcePower * 0.2f, ForceMode.Impulse);

        Debug.Log($"Скорость мяча: {ballRigidbody.velocity.magnitude:F2} м/с");
        Debug.Log($"Угловая скорость: {ballRigidbody.angularVelocity.magnitude:F2} рад/с");
        Debug.Log("------------------------");

        currentState = ShootState.Idle;
        arrowObject.SetActive(false);
        currentForce = 0f;
    }

    void CheckBallPosition()
    {
        if (ballRigidbody.position.y < 0.03f)
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}