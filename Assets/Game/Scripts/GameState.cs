using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
