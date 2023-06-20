using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public TMP_Text scoreText;

    public TMP_Text victoryText;

    public TMP_Text highScoreText;

    public int highScore = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("EmojiShooterHighScore", 0);
        string formattedScore = highScore.ToString("D4");
        highScoreText.text = $"HI {formattedScore}";
    }

    void Update()
    {
        string formattedScore = score.ToString("D4");
        scoreText.text = $"{formattedScore}";
        if (score == 1500)
        {
            win();
            victoryText.gameObject.SetActive(true);
        }

        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("EmojiShooterHighScore", highScore);
            PlayerPrefs.Save();
            string formattedHighScore = highScore.ToString("D4");
            highScoreText.text = $"HI {formattedHighScore}";
        }
    }

    public static void AddScore()
    {
        score += 25;
    }

    public static void ResetScore()
    {
        score = 0;
    }

    public void win()
    {
        if (!checkIfAlreadyWon())
        {
            int completedMissions = PlayerPrefs.GetInt(StatsManager.FRIENDPREF, 0);
            completedMissions++;
            StatsManager.updatePref(StatsManager.FRIENDPREF, completedMissions);
            PlayerPrefs.SetInt("EmojiShooter", 1);
            PlayerPrefs.Save();
        }
    }

    public bool checkIfAlreadyWon()
    {
        return PlayerPrefs.HasKey("EmojiShooter");
    }
}
