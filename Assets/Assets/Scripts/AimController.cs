using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [Header("Настройки камеры")]
    public float normalFOV = 60f;        // Обычный угол обзора
    public float aimFOV = 40f;           // Угол обзора при активации прицеливания
    public Vector3 sensePositionOffset;  // Смещение камеры при 
    public float smoothTime = 0.3f;      // Время для сглаживания переходов

    private Vector3 normalPositionOffset; // Обычное смещение камеры
    private bool isSenseActive = false;  // Активно ли прицеливание
    private float _targetFOV;            // Целевой угол обзора
    private Vector3 _targetPosition;     // Целевая позиция для смещения камеры
    private Vector3 _velocity = Vector3.zero; // Скорость для плавного смещения

    private void Start()
    {
        // Устанавливаем начальные значения камеры
        _targetFOV = normalFOV;
        normalPositionOffset = _camera.transform.position;
        _targetPosition = normalPositionOffset;
        sensePositionOffset = normalPositionOffset + new Vector3(0, 0.5f, 0f); //слегка приподнимаем камеру при прицеливании
    }

    private void Update()
    {
        // Проверяем нажатие правой кнопки мыши
        if (Input.GetMouseButtonDown(1))
        {
            ActivateAim();
        }
        else if (Input.GetMouseButtonUp(1)) // Отпускание правой кнопки мыши
        {
            DeactivateAim();
        }

        // Плавно изменяем угол обзора (FOV)
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _targetFOV, Time.deltaTime / smoothTime);

        // Плавно изменяем позицию камеры
        _camera.transform.localPosition = Vector3.SmoothDamp(_camera.transform.localPosition, _targetPosition, ref _velocity, smoothTime);
    }

    private void ActivateAim()
    {
        isSenseActive = true;
        _targetFOV = aimFOV;
        _targetPosition = sensePositionOffset;
    }

    private void DeactivateAim()
    {
        isSenseActive = false;
        _targetFOV = normalFOV;
        _targetPosition = normalPositionOffset;
    }
}
