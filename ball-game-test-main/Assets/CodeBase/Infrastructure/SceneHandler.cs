namespace Infrastructure
{
    public class SceneHandler
    {
        public void LoadLevel(string sceneName) => 
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
