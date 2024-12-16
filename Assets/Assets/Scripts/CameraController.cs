using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [Header ("Настройки движения и ограничения угла обзора")]
    [SerializeField] private float _smoothTime = 0.1f;   // время для сглаживания движения
    [SerializeField] private float _XAngle;              //  угол по горизонтали
    [Range(50, 500)]
    [SerializeField] float _sensivity = 400;

    private float _rotationX;
    private float _rotationY;

    private float _currentRotationX = 0f; // текущее значение для плавного вращения по горизонтали
    private float _currentRotationY = 0f; // текущее значение для плавного вращения по вертикали

    private void Start()
    {
        //блокировка курсора при старте сцены
        MouseCursorLock();
    }

    private void Update()
    {
        RotateCamera();
    }
    private void RotateCamera()
    {
        //получаем координаты мыши
        float mouseX = Input.GetAxis("Mouse X") * _sensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _sensivity * Time.deltaTime;

        // Поворот вверх-вниз (ограничен от -90 до 90)
        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        _rotationY += mouseX;
        _rotationY = Mathf.Clamp(_rotationY, -_XAngle, _XAngle);

        // Плавно изменяем значения углов поворота
        _currentRotationX = Mathf.Lerp(_currentRotationX, _rotationX, _smoothTime);
        _currentRotationY = Mathf.Lerp(_currentRotationY, _rotationY, _smoothTime);

        // Применяем поворот камеры
        _cameraTransform.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);

    }

    private void MouseCursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }






}
