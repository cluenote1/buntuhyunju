using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;        // 현재 점수를 표시할 UI Text
    public Text bestScoreText;    // 최고 점수를 표시할 UI Text

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

        DataManager.Instance.ResetScore();  // 초기화
    }

    void Update()
    {
        if (DataManager.Instance != null) // DataManager.Instance가 null인지 확인
        {
            scoreText.text = "Score: " + DataManager.Instance.GetScore().ToString();
            bestScoreText.text = "Best Score: " + DataManager.bestScore.ToString();
        }
        
    }
}
