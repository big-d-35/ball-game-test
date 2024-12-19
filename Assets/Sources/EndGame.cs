using UnityEngine;

public class EndGame : MonoBehaviour
{

    [SerializeField] private LevelState levelState;
    private const int LIMIT = 500;
    private int counter = 0;
    private bool isEnter = false;
    void OnTriggerEnter(Collider collider)
    {
        isEnter = true;
    }

    void OnTriggerExit(Collider collider)
    {
        isEnter = false;
        counter = 0;
    }

    void Update()
    {
        CheckEnd();
    }

    private void CheckEnd()
    {
        if (isEnter)
        {
            counter++;
            if (counter == LIMIT)
            {
                counter = 0;
                levelState.RestartLevel();
            }
        }
    }
}
