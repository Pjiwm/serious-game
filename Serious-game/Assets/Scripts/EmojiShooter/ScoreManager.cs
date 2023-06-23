using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int Score;
    public TMP_Text scoreText;

    public TMP_Text victoryText;

    public TMP_Text highScoreText;

    public int highScore = 0;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt(PlayerPrefKeys.EmojiShooterHighscore, 0);
        var formattedScore = highScore.ToString("D4");
        highScoreText.text = $"HI {formattedScore}";
    }

    void Update()
    {
        var formattedScore = Score.ToString("D4");
        scoreText.text = $"{formattedScore}";
        if (Score == 1500)
        {
            Win();
            victoryText.gameObject.SetActive(true);
        }

        if (Score > highScore) {
            highScore = Score;
            PlayerPrefs.SetInt(PlayerPrefKeys.EmojiShooterHighscore, highScore);
            PlayerPrefs.Save();
            var formattedHighScore = highScore.ToString("D4");
            highScoreText.text = $"HI {formattedHighScore}";
        }
    }

    public static void AddScore()
    {
        Score += 25;
    }

    public static void ResetScore()
    {
        Score = 0;
    }

    public static void Win()
    {
        if (!CheckIfAlreadyWon())
        {
            int completedMissions = PlayerPrefs.GetInt(PlayerPrefKeys.Friends, 0);
            completedMissions++;
            StatsManager.UpdatePref(PlayerPrefKeys.Friends, completedMissions);
            PlayerPrefs.SetInt("EmojiShooter", 1);
            PlayerPrefs.Save();
        }
    }

    public static bool CheckIfAlreadyWon()
    {
        return PlayerPrefs.HasKey(PlayerPrefKeys.EmojiShooter);
    }
}
