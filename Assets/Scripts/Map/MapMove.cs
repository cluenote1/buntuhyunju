using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    // 여러 맵 조각 프리팹들을 담을 배열
    public GameObject[] mapPieces;

    // 첫 번째 맵 조각을 지정할 변수 (사용자가 직접 설정)
    public GameObject firstMapPiece;

    // 맵 조각이 생성될 시작 위치
    public Transform spawnPoint;

    // 생성 간격 (X축 기준으로 이동할 거리)
    public float spawnDistance = 10f;

    // 이동 속도
    public float moveSpeed = 5f;

    // 첫 번째 조각을 이미 생성했는지 체크할 플래그
    private bool isFirstPieceGenerated = false;

    void Start()
    {
        // 일정 시간마다 프리팹 생성 (예: 2초마다)
        InvokeRepeating("SpawnMapPiece", 0f, 2f);
    }

    void SpawnMapPiece()
    {
        if (!isFirstPieceGenerated)
        {
            // 첫 번째 맵 조각은 무조건 사용자가 설정한 프리팹으로 생성
            Instantiate(firstMapPiece, spawnPoint.position, Quaternion.identity);

            // 첫 번째 조각에 이동 스크립트가 있는지 확인하고, 속도를 설정
            MapPieceMover mover = firstMapPiece.GetComponent<MapPieceMover>();
            if (mover != null)
            {
                mover.moveSpeed = moveSpeed;
            }

            // 첫 번째 조각 생성 완료
            isFirstPieceGenerated = true;
        }
        else
        {
            // 첫 번째 조각 이후는 랜덤으로 배열에서 프리팹 선택
            int randomIndex = Random.Range(0, mapPieces.Length);
            GameObject selectedPiece = mapPieces[randomIndex];

            // 프리팹 생성
            GameObject newPiece = Instantiate(selectedPiece, spawnPoint.position, Quaternion.identity);

            // 맵 조각에 이동 스크립트가 있는지 확인하고, 속도를 설정
            MapPieceMover mover = newPiece.GetComponent<MapPieceMover>();
            if (mover != null)
            {
                mover.moveSpeed = moveSpeed;
            }
        }

        // 생성된 후 다음 생성 위치로 이동
        spawnPoint.position += new Vector3(spawnDistance, 0, 0);
    }
}
