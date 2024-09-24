using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField] private Transform playerHand; // �÷��̾� �� ��ġ
    [SerializeField] private float shotSpeed = 10f; // �߻� �ӵ�
    [SerializeField] private float maxDistance = 9f; // �ִ� �Ÿ�
    [SerializeField] private AudioClip shotSound; // �߻� �Ҹ�
    [SerializeField] private LayerMask wireBuilding; // ���̾ ����� ���� ���̾�
    [SerializeField] private LineRenderer lineRenderer; // ���� ������

    private bool isWire; // ���� ���̾� ����
    private RaycastHit2D hit; // 2D RaycastHit

    private float lastClickTime; // ������ Ŭ�� �ð�
    private int clickCount; // Ŭ�� ī��Ʈ
    private float clickDelay = 0.3f; // �� �� Ŭ�� ����

    private void Start()
    {
        DisableWire();
    }

    private void Update()
    {
        // ��Ŭ�� ����
        if (Input.GetMouseButtonDown(0)) // 0�� ��Ŭ��
        {
            if (Time.time - lastClickTime <= clickDelay) // �� �� Ŭ���� ���� ���� �ִ��� Ȯ��
            {
                clickCount++;
                if (clickCount == 2) // �� �� Ŭ��
                {
                    ShotWire(); // ���̾� �߻�
                    clickCount = 0; // ī��Ʈ �ʱ�ȭ
                }
            }
            else
            {
                clickCount = 1; // ī��Ʈ �ʱ�ȭ �� 1�� ����
            }
            lastClickTime = Time.time; // ������ Ŭ�� �ð� ������Ʈ
        }

        // ���̾� ��������Ʈ�� ��ġ�� �÷��̾��� �� ��ġ�� ����ȭ
        if (isWire)
        {
            UpdateLineRenderer();
        }
    }

    public void ShotWire()
    {
        if (isWire) return;
        SoundManager.Instance.SFXPlay("ShotWire", shotSound);
        isWire = true;
        lineRenderer.enabled = true; // ���� ������ Ȱ��ȭ
        StartCoroutine("Shot");
    }

    public void DisableWire()
    {
        isWire = false;
        lineRenderer.enabled = false; // ���� ������ ��Ȱ��ȭ
        transform.parent = null;
    }

    private IEnumerator Shot()
    {
        Vector3 lastHandPos = playerHand.position;
        Vector3 targetPos = lastHandPos + (Vector3.right * maxDistance); // ���ϴ� �������� ����, ���⼭ Vector3.right�� ���ϴ� �������� �ٲ� �� ����

        // ���̾ ���ư��� ����
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * shotSpeed);
            yield return null;
        }

        // ��ǥ�� ���� �� Raycast�� ����
        hit = Physics2D.Raycast(lastHandPos, (targetPos - lastHandPos).normalized, maxDistance, wireBuilding);
        if (hit.collider != null)
        {
            Target();
        }
        DisableWire(); // ���̾� ��Ȱ��ȭ
    }

    private void Target()
    {
        transform.parent = hit.transform; // 2D���� �θ�� ����
    }

    private void UpdateLineRenderer()
    {
        // ���� �������� ���۰� �� ��ġ ����
        lineRenderer.SetPosition(0, playerHand.position);
        lineRenderer.SetPosition(1, transform.position);
    }
}
