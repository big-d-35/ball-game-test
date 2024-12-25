using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.BallLogic
{
    public class Ball : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Ground ground))
                ResetScene();
        }

        private void ResetScene() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}