using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float scoreMultiplier = 1;
    float score;
    public const string HighScoreKey = "HighScore";

    void Update()
    {
        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }
}
