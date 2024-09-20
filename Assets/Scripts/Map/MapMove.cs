using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public GameObject[] mapPieces;
    public GameObject firstMapPiece;
    public Transform spawnPoint;

    // 고정된 간격
    public float fixedSpawnDistance = 10f; // 원하는 간격 설정

    void Start()
    {
        // 첫 번째 맵 조각 생성
        Instantiate(firstMapPiece, spawnPoint.position, Quaternion.identity);

        // spawnPoint를 초기 간격만큼 이동
        spawnPoint.position += new Vector3(fixedSpawnDistance, 0, 0);

        // 일정 시간마다 프리팹 생성
        InvokeRepeating("SpawnMapPiece", 0f, 2f);
    }

    void SpawnMapPiece()
    {
        // 랜덤으로 프리팹 선택
        int randomIndex = Random.Range(0, mapPieces.Length);
        GameObject selectedPiece = mapPieces[randomIndex];

        // 프리팹 생성
        Instantiate(selectedPiece, spawnPoint.position, Quaternion.identity);

        // spawnPoint를 고정된 간격만큼 이동
        spawnPoint.position += new Vector3(fixedSpawnDistance, 0, 0);
    }
}
