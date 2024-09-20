using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPieceMover : MonoBehaviour
{
    // �̵� �ӵ�
    public float moveSpeed = 5f;

    void Update()
    {
        // �������� �̵�
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // ���� ��ġ�� ������ �� ���� ���� (��: X ��ǥ�� -20�̸� ����)
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
}
