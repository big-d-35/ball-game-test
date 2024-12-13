using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private Text score;
    private Ball ball;
    int goals;
    int misses;
    private void Start()
    {
        score = GetComponent<Text>();
        ball = Ball.instance;
        ball.onThrow += OnScoreChanged;
    }
    // Этот метод добавляет очко
    public void OnScoreChanged(bool isHit)
    {
        switch (isHit)
        {
            case true:
                goals++;
                break;
            case false:
                misses++;
                break;
        }
        score.text = string.Format("{0} : {1}",goals,misses);
    }
}