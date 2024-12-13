using UnityEngine;
public class SoundManager : MonoBehaviour
{
    private Ball ball;
    private AudioSource gameOverSound;
    void Start()
    {
        gameOverSound = GetComponent<AudioSource>();
        ball = Ball.instance;
        ball.onThrow += OnGameOver;
    }
    private void OnGameOver(bool isWin)
    {
        switch (isWin)
        {
            case true:
                gameOverSound.Play();
                break;
            case false:
                print("Проигрыш!");
                break;
        }
    }
}