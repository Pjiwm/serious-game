using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
        public  static int score = 0;
        public TMP_Text scoreText;

    void Start()
    {
        // scoreText.text = $"Score: {Scoscore}";
    }

    void Update()
    {
        string formattedScore = score.ToString("D4");
        scoreText.text = $"{formattedScore}";
        Debug.Log("SCORE " + formattedScore);
    }

    public static void AddScore()
    {
        score += 25;
    }

    public static void ResetScore()
    {
        score = 0;
    }
}
