using UnityEngine;
using TMPro; // Подключаем TextMeshPro пространство имён

public class BallGoalTracker : MonoBehaviour
{
    private bool _inColliderPassed; // Было ли пройдено InCollider
    private bool _goalScored;       // Было ли зафиксировано попадание

    [SerializeField] private TextMeshProUGUI _goalText; // Текст для отображения "ГОЛ!"

    private void Start()
    {
        // Убедимся, что текст выключен в начале
        _goalText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверка прохождения верхнего триггера (InCollider)
        if (other.CompareTag("InCollider"))
        {
            _inColliderPassed = true;
            Debug.Log("Мяч прошёл через InCollider");
        }

        // Проверка прохождения нижнего триггера (OutCollider)
        if (other.CompareTag("OutCollider") && _inColliderPassed && !_goalScored)
        {
            _goalScored = true;
            Debug.Log("Гол засчитан!");

            // Здесь можно вызвать метод фиксации гола
            OnGoalScored();

            // Сброс флагов для последующего броска
            ResetGoalTracking();
        }
    }

    /// <summary>
    /// Метод, который вызывается при засчитывании гола.
    /// Здесь можно отобразить текст на экране.
    /// </summary>
    private void OnGoalScored()
    {
        // Включаем текст "ГОЛ!"
        _goalText.gameObject.SetActive(true);

        // Через 2 секунды выключаем текст "ГОЛ!"
        Invoke(nameof(HideGoalText), 2f);
    }

    /// <summary>
    /// Метод для отключения текста "ГОЛ!"
    /// </summary>
    private void HideGoalText()
    {
        _goalText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Сброс всех флагов, чтобы мяч мог снова фиксировать прохождение триггеров.
    /// </summary>
    private void ResetGoalTracking()
    {
        _inColliderPassed = false;
        _goalScored = false;
    }

    /// <summary>
    /// Сбросить состояние мяча (например, при перезапуске сцены).
    /// </summary>
    public void ResetBall()
    {
        ResetGoalTracking();
    }
}
