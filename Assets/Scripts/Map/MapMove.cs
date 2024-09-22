using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public GameObject[] mapPieces;       // �������� ������ �� ���� �迭
    public GameObject firstMapPiece;     // ù ��°�� ������ �� ����
    public Transform spawnPoint;         // �� ������ ������ ��ġ
    public float fixedSpawnDistance = 10f; // �÷��� ������ ������ ����

    private GameObject lastMapPiece;     // ���������� ������ �� ������ ������ ����
    private int lastIndex = -1;          // ���������� ������ ������ �ε���

    void Start()
    {
        // ù ��° �� ���� ����
        lastMapPiece = Instantiate(firstMapPiece, spawnPoint.position, Quaternion.identity);
        spawnPoint.position += new Vector3(fixedSpawnDistance, 0, 0); // ���� �÷��� ��ġ�� �̵�

        // �� ��° �÷��� ����
        SpawnSecondMapPiece();

        // ���� ���� �� ���� ���� ����
        InvokeRepeating("SpawnMapPiece", 2f, 2f);
    }

    void SpawnSecondMapPiece()
    {
        // �� ��° �� ������ ������ mapPieces[0]���� ����
        GameObject secondPiece = mapPieces[0];
        lastMapPiece = Instantiate(secondPiece, spawnPoint.position, Quaternion.identity);
        spawnPoint.position += new Vector3(fixedSpawnDistance, 0, 0); // ���� �÷��� ��ġ�� �̵�
    }

    void SpawnMapPiece()
    {
        int randomIndex;

        // ������ ���� �ε����� �������� �ʱ� ���� ����
        do
        {
            randomIndex = Random.Range(0, mapPieces.Length);
        } while (randomIndex == lastIndex); // ���� �ε����� ������ �ٽ� ����

        GameObject selectedPiece = mapPieces[randomIndex];

        // �� �� ���� ����
        lastMapPiece = Instantiate(selectedPiece, spawnPoint.position, Quaternion.identity);
        lastIndex = randomIndex; // ������ ������ ������ �ε��� ������Ʈ

        // spawnPoint ������Ʈ
        spawnPoint.position += new Vector3(fixedSpawnDistance, 0, 0); // ���� �÷��� ��ġ�� �̵�
    }
}
