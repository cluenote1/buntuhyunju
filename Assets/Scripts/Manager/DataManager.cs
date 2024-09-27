using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            return instance;
        }
    }

    public int score = 0;      // ���� ����
    public static int bestScore = 0;  // �ְ� ����
    public int Stage = 0;
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(int points)
    {
        score += points;  // ���� �߰�
        if (score > bestScore) // ���� ������ �ְ� �������� Ŭ ���
        {
            bestScore = score; // �ְ� ���� ������Ʈ
        }
    }

    public void ResetScore()

    {
        score = 0;  // ���� �ʱ�ȭ
    }

    public int GetScore()
    {
        return score;
    }

    // ���� ����� �� ȣ��
    public void RestartGame()
    {
        ResetScore();  // ������ 0���� �ʱ�ȭ
        SceneManager.LoadScene("GameScene");  // ���� �� �ٽ� �ε�
    }

    public void CheckAndUpdateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score; // ����Ʈ ���ھ� ������Ʈ
            PlayerPrefs.SetInt("BestScore", bestScore); // PlayerPrefs�� ����
            PlayerPrefs.Save(); // ����
        }
    }
}
