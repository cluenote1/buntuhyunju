using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPieceMover : MonoBehaviour
{
    // 이동 속도
    public float moveSpeed = 5f;

    void Update()
    {
        // 점수에 따라 이동 속도 조절
        moveSpeed = GetScoreBasedSpeed();

        // 왼쪽으로 이동
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // 일정 위치를 지나면 맵 조각 제거 (예: X 좌표가 -20이면 제거)
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    float GetScoreBasedSpeed()
    {
        // 예시: 1000점마다 속도 증가
        int score = DataManager.Instance.GetScore(); // 현재 점수를 가져오는 함수
        return 5f + (score / 400) * 2f; // 기본 속도 + 점수에 따라 증가
    }
}
