using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public GameObject[] mapPieces;
    public GameObject firstMapPiece;
    public Transform spawnPoint;

    // ������ ����
    public float fixedSpawnDistance = 10f; // ���ϴ� ���� ����

    void Start()
    {
        // ù ��° �� ���� ����
        Instantiate(firstMapPiece, spawnPoint.position, Quaternion.identity);

        // spawnPoint�� �ʱ� ���ݸ�ŭ �̵�
        spawnPoint.position += new Vector3(fixedSpawnDistance, 0, 0);

        // ���� �ð����� ������ ����
        InvokeRepeating("SpawnMapPiece", 0f, 2f);
    }

    void SpawnMapPiece()
    {
        // �������� ������ ����
        int randomIndex = Random.Range(0, mapPieces.Length);
        GameObject selectedPiece = mapPieces[randomIndex];

        // ������ ����
        Instantiate(selectedPiece, spawnPoint.position, Quaternion.identity);

        // spawnPoint�� ������ ���ݸ�ŭ �̵�
        spawnPoint.position += new Vector3(fixedSpawnDistance, 0, 0);
    }
}
