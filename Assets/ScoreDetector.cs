using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDetector : MonoBehaviour
{
    public Transform ballTransform; // Ссылка на мяч
    public string ballTag = "Ball"; // Тег мяча для поиска
    public float detectionRadius; // Максимальное расстояние для засчитывания очка
    public float resetDistance = 0.2f; // Расстояние, на котором мяч должен отдалиться для нового гола
    public bool canScore = true; // Флаг готовности к новому голу
    private int score = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // Подписываемся на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Вызывается при загрузке сцены
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindBall();
        canScore = true;
    }

    void Start()
    {
        FindBall();
        canScore = true;
    }

    // Метод для поиска мяча
    void FindBall()
    {
        // Пробуем найти мяч по тегу
        GameObject ball = GameObject.FindGameObjectWithTag(ballTag);
        if (ball != null)
        {
            ballTransform = ball.transform;
            Debug.Log("Мяч найден");
        }
        else
        {
            Debug.LogWarning("Мяч не найден! Проверьте, что у мяча установлен правильный тег.");
        }
    }

    void Update()
    {
        // Если мяч не найден, пытаемся найти его
        if (ballTransform == null)
        {
            FindBall();
            return;
        }

        float distance = Vector3.Distance(ballTransform.position, transform.position);

        if (canScore && IsBallInsideRadius())
        {
            Debug.Log("Очко засчитано!");
            canScore = false;
            score++;
            Debug.Log($"Текущий счет: {score}");
        }
        else if (!canScore && distance > resetDistance)
        {
            canScore = true;
            Debug.Log("Готов к новому голу");
        }
    }

    private bool IsBallInsideRadius()
    {
        if (ballTransform == null) return false;
        float distance = Vector3.Distance(ballTransform.position, transform.position);
        return distance <= detectionRadius;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, resetDistance);
    }
}