using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    // ���� �� ���� �����յ��� ���� �迭
    public GameObject[] mapPieces;

    // ù ��° �� ������ ������ ���� (����ڰ� ���� ����)
    public GameObject firstMapPiece;

    // �� ������ ������ ���� ��ġ
    public Transform spawnPoint;

    // ���� ���� (X�� �������� �̵��� �Ÿ�)
    public float spawnDistance = 10f;

    // �̵� �ӵ�
    public float moveSpeed = 5f;

    // ù ��° ������ �̹� �����ߴ��� üũ�� �÷���
    private bool isFirstPieceGenerated = false;

    void Start()
    {
        // ���� �ð����� ������ ���� (��: 2�ʸ���)
        InvokeRepeating("SpawnMapPiece", 0f, 2f);
    }

    void SpawnMapPiece()
    {
        if (!isFirstPieceGenerated)
        {
            // ù ��° �� ������ ������ ����ڰ� ������ ���������� ����
            Instantiate(firstMapPiece, spawnPoint.position, Quaternion.identity);

            // ù ��° ������ �̵� ��ũ��Ʈ�� �ִ��� Ȯ���ϰ�, �ӵ��� ����
            MapPieceMover mover = firstMapPiece.GetComponent<MapPieceMover>();
            if (mover != null)
            {
                mover.moveSpeed = moveSpeed;
            }

            // ù ��° ���� ���� �Ϸ�
            isFirstPieceGenerated = true;
        }
        else
        {
            // ù ��° ���� ���Ĵ� �������� �迭���� ������ ����
            int randomIndex = Random.Range(0, mapPieces.Length);
            GameObject selectedPiece = mapPieces[randomIndex];

            // ������ ����
            GameObject newPiece = Instantiate(selectedPiece, spawnPoint.position, Quaternion.identity);

            // �� ������ �̵� ��ũ��Ʈ�� �ִ��� Ȯ���ϰ�, �ӵ��� ����
            MapPieceMover mover = newPiece.GetComponent<MapPieceMover>();
            if (mover != null)
            {
                mover.moveSpeed = moveSpeed;
            }
        }

        // ������ �� ���� ���� ��ġ�� �̵�
        spawnPoint.position += new Vector3(spawnDistance, 0, 0);
    }
}
