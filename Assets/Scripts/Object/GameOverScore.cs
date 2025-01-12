﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    public Text scoreText;
    public Text bestScoreText;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned!");
        }

        if (bestScoreText == null)
        {
            Debug.LogError("Best Score Text is not assigned!");
        }

        int currentScore = DataManager.Instance.GetScore();
        scoreText.text = "分数: " + currentScore.ToString();
        bestScoreText.text = "最佳成绩: " + DataManager.bestScore.ToString();
    }
}
