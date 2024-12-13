using UnityEngine;

public class FloorDetecter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Invoke("ReloadScene", 2f);
    }

    void ReloadScene()
    {
        ScoreCounter.Instance.ReloadScene();
    }
}
