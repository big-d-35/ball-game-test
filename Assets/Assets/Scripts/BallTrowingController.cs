using UnityEngine;
using UnityEngine.UI;

public class BallTrowingController : MonoBehaviour
{
    [SerializeField] private Slider _ballTrowSlider; // Слайдер для отображения силы броска
    [SerializeField] private float _timePower = 2f;  // Время, за которое сила увеличивается до максимума
    [SerializeField] private Ball _ball;
    [SerializeField] private Rigidbody _ballRigidbody;
    private float _trowPower; // Текущая сила броска
    private float _currentTime; // Время, в течение которого зажата кнопка
    

    void Update()
    {
        if (Input.GetMouseButton(0)) // Зажатие кнопки мыши
        {
            if (_ball._isPlaying == false)
            {
                SetPowerValue();
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Отпускание кнопки мыши
        {
            _ball.TrowForce(_trowPower);
            //ResetPowerValue();
        }
    }

    private void SetPowerValue()
    {
        _currentTime += Time.deltaTime; // Накопление времени
        float normalizedTime = Mathf.Clamp01(_currentTime / _timePower); // Ограничиваем от 0 до 1
        _trowPower = Mathf.Lerp(_ballTrowSlider.minValue, _ballTrowSlider.maxValue, normalizedTime); // Интерполяция
        _ballTrowSlider.value = _trowPower; // Обновляем значение слайдера
    }

    public void ResetPowerValue()
    {
        //Debug.Log($"Сила броска: {_trowPower}");
        _currentTime = 0f; // Сбрасываем накопленное время
        _trowPower = _ballTrowSlider.minValue; // Сбрасываем силу броска
        _ballTrowSlider.value = _trowPower; // Сбрасываем значение слайдера
    }

}
