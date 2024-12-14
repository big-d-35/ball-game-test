using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour
{
    private void OnEnable()
    {
        BallStopEvent.OnStop += Reset;
    }

    private void OnDisable()
    {
        BallStopEvent.OnStop -= Reset;
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
