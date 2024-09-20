using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPieceMover : MonoBehaviour
{
    // 이동 속도
    public float moveSpeed = 5f;

    void Update()
    {
        // 왼쪽으로 이동
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // 일정 위치를 지나면 맵 조각 제거 (예: X 좌표가 -20이면 제거)
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}
