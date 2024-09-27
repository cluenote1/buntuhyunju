using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;        // ���� ������ ǥ���� UI Text
    public Text bestScoreText;    // �ְ� ������ ǥ���� UI Text

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

        DataManager.Instance.ResetScore();  // �ʱ�ȭ
    }

    void Update()
    {
        if (DataManager.Instance != null) // DataManager.Instance�� null���� Ȯ��
        {
            scoreText.text = "Score: " + DataManager.Instance.GetScore().ToString();
            bestScoreText.text = "Best Score: " + DataManager.bestScore.ToString();
        }
        
    }
}
