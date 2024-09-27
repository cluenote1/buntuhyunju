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

    public int score = 0;      // 현재 점수
    public static int bestScore = 0;  // 최고 점수
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
        score += points;  // 점수 추가
        if (score > bestScore) // 현재 점수가 최고 점수보다 클 경우
        {
            bestScore = score; // 최고 점수 업데이트
        }
    }

    public void ResetScore()

    {
        score = 0;  // 점수 초기화
    }

    public int GetScore()
    {
        return score;
    }

    // 게임 재시작 시 호출
    public void RestartGame()
    {
        ResetScore();  // 점수를 0으로 초기화
        SceneManager.LoadScene("GameScene");  // 게임 씬 다시 로드
    }

    public void CheckAndUpdateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score; // 베스트 스코어 업데이트
            PlayerPrefs.SetInt("BestScore", bestScore); // PlayerPrefs에 저장
            PlayerPrefs.Save(); // 저장
        }
    }
}
