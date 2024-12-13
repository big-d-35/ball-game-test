using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;
    public int score = 0;
    private const string ScoreKey = "Score";
    public TextMeshProUGUI scoreText;
    public Image fadeImage;
    private bool isReloading = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        score = PlayerPrefs.GetInt(ScoreKey, 0);
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.Save();
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void ReloadScene()
    {
        if (isReloading) return;
        isReloading = true;
        StartCoroutine(ReloadSceneWithFade());
    }

    private IEnumerator ReloadSceneWithFade()
    {
        float fadeDuration = 1f;
        yield return FadeScreen(1f, fadeDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return new WaitForSeconds(0.1f);
        yield return FadeScreen(0f, fadeDuration);
        isReloading = false;
    }

    private IEnumerator FadeScreen(float targetAlpha, float duration)
    {
        float startAlpha = fadeImage.color.a;
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / duration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}
