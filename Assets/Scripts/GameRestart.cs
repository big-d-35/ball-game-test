using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    private void OnEnable()
    {
        _ball.Fallen += OnFallen;
    }

    private void OnDisable()
    {
        _ball.Fallen -= OnFallen;  
    }

    private void OnFallen()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
