using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] private Transform _startBallPosition;          //ссылка на позицию мяча
    [SerializeField] private Transform _camera;                     //ссылка на камеру
    [SerializeField] private BallTrowingController _ballController; //ссылка на мяч и его контроллер
    [Header("Установить силу подъема мяча")]
    [SerializeField, Range(1, 3)] private float _verticalPower;

    private Vector3 _startPosition; // Переменная для сохранения стартовой позиции мяча
    private int countBallReset;     //счетчик столкновений мяча с землей перед сбросом

    public bool _isPlaying;
    private void Start()
    {
        _isPlaying = false;
        // Запоминаем стартовую позицию
        GetStartPositionBall();
        // Включаем кинематику чтобы мяч не падал
        OnKinematic();
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            ResetBall();
        }
    }

    private void OnKinematic()
    {
        _rb.isKinematic = true;
    }
    private void OffKinematic()
    {
        _rb.isKinematic = false;
    }

    private void GetStartPositionBall() //получить стартовую позицию мяча
    {
        // Сохраняем текущую позицию мяча как стартовую
        _startPosition = transform.position;
    }

    private void SetStartPositionBall() //вернуть позицию мяча
    {
        // Возвращаем мяч в сохраненную стартовую позицию
        transform.position = _startPosition;
        transform.rotation = _startBallPosition.rotation; // При необходимости сбрасываем и вращение мяча

        //// нужно сбросить скорость мяча
        //_rb.linearVelocity = Vector3.zero; // Сбрасываем скорость
        //_rb.angularVelocity = Vector3.zero; // Сбрасываем угловую скорость
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулся ли мяч с землей
        if (collision.gameObject.CompareTag("Ground"))
        {
            countBallReset++; // Увеличиваем счетчик отскоков

            // Если мяч отскочил 3 раза, сбрасываем его в начальную позицию
            if (countBallReset > 3)
            {
                ResetBall();
            }
        }
    }

    public void TrowForce(float power)
    {
        if (_isPlaying == false) //если мяч не в игре, то бросок разрешен
        {
            OffKinematic();
            // Получаем угол поворота камеры по оси Y
            float cameraYAngle = _camera.transform.eulerAngles.y;

            // Применяем угол Y камеры к целевому объекту
            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.y = cameraYAngle / 2;  // При необходимости можно уменьшить угол
            transform.eulerAngles = targetRotation;

            // Сила по горизонтали и вертикали
            float horizontalPower = power; // Сила по горизонтали (для дальности)
            float verticalPower = power * _verticalPower; // Сила по вертикали (для подъема)

            // Применяем угол для вычисления силы по оси X и Z
            float angleInRadians = cameraYAngle * Mathf.Deg2Rad; // Преобразуем угол в радианы

            // Расчет силы по осям Z и X на основе угла камеры (меняем оси)
            float forceZ = Mathf.Cos(angleInRadians) * horizontalPower; // сила по оси Z
            float forceX = Mathf.Sin(angleInRadians) * horizontalPower; // сила по оси X

            // Устанавливаем силу вектора
            Vector3 throwForce = new Vector3(forceX, verticalPower, forceZ);

            // Бросок мяча
            _rb.AddForce(throwForce, ForceMode.VelocityChange);

            _isPlaying = true;//мяч в игре
        }
        
    }

    public void ResetBall()
    {
        OnKinematic(); // Применяем кинематику 
        SetStartPositionBall(); // Устанавливаем начальную позицию мяча
        countBallReset = 0; // Сбрасываем счетчик
        _isPlaying = false; //мяч не в игре
        _ballController.ResetPowerValue(); //сброс параметров мяча
    }

}
