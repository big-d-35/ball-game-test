using UnityEngine;

public class SceneManager : MonoBehaviour
{
	public void ChangeScene(int numberScene)
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene(numberScene);
	}
}
