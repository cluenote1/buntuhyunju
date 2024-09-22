using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public GameObject[] mapPieces;       // 랜덤으로 생성될 맵 조각 배열
    public GameObject firstMapPiece;     // 첫 번째로 생성할 맵 조각
    public Transform spawnPoint;         // 맵 조각이 생성될 위치
    public float fixedSpawnDistance = 10f; // 플랫폼 사이의 고정된 간격

    private GameObject lastMapPiece;     // 마지막으로 생성된 맵 조각을 저장할 변수
    private int lastIndex = -1;          // 마지막으로 생성된 조각의 인덱스

    void Start()
    {
        // 첫 번째 맵 조각 생성
        lastMapPiece = Instantiate(firstMapPiece, spawnPoint.position, Quaternion.identity);
        spawnPoint.position += new Vector3(fixedSpawnDistance, 0, 0); // 다음 플랫폼 위치로 이동

        // 두 번째 플랫폼 생성
        SpawnSecondMapPiece();

        // 이후 랜덤 맵 조각 생성 시작
        InvokeRepeating("SpawnMapPiece", 2f, 2f);
    }

    void SpawnSecondMapPiece()
    {
        // 두 번째 맵 조각은 무조건 mapPieces[0]으로 설정
        GameObject secondPiece = mapPieces[0];
        lastMapPiece = Instantiate(secondPiece, spawnPoint.position, Quaternion.identity);
        spawnPoint.position += new Vector3(fixedSpawnDistance, 0, 0); // 다음 플랫폼 위치로 이동
    }

    void SpawnMapPiece()
    {
        int randomIndex;

        // 이전과 같은 인덱스를 선택하지 않기 위한 로직
        do
        {
            randomIndex = Random.Range(0, mapPieces.Length);
        } while (randomIndex == lastIndex); // 이전 인덱스와 같으면 다시 선택

        GameObject selectedPiece = mapPieces[randomIndex];

        // 새 맵 조각 생성
        lastMapPiece = Instantiate(selectedPiece, spawnPoint.position, Quaternion.identity);
        lastIndex = randomIndex; // 마지막 생성된 조각의 인덱스 업데이트

        // spawnPoint 업데이트
        spawnPoint.position += new Vector3(fixedSpawnDistance, 0, 0); // 다음 플랫폼 위치로 이동
    }
}
