using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField] private Transform playerHand; // �÷��̾� �� ��ġ
    [SerializeField] private float shotSpeed = 10f; // �߻� �ӵ�
    [SerializeField] private float retractSpeed = 15f; // �ǵ��� �ӵ� (�⺻������ �� ������ ����)
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
        // ���� ������ �ʱ� ����
        lineRenderer.startWidth = 0.5f; // ���� �β�
        lineRenderer.endWidth = 0.5f; // �� �β�
        lineRenderer.startColor = Color.red; // ���� ����
        lineRenderer.endColor = Color.red; // �� ����

        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // ���� ����
        transform.position = playerHand.position; // �ʱ� ��ġ ����
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

        // ���̾� ���¿� ���� ��ġ ������Ʈ
        if (isWire)
        {
            UpdateLineRenderer();
        }
        else
        {
            // ���̾ ��Ȱ��ȭ�� ��쿡�� �÷��̾� �� ��ġ�� ���󰡵��� ������Ʈ
            if (playerHand != null)
            {
                transform.position = playerHand.position; // ���̾� ��ġ�� �÷��̾� �� ��ġ�� ������Ʈ
            }
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

        // 2�� �������� ��ǥ ����
        Vector3 targetDirection = new Vector3(1, 1, 0).normalized;
        Vector3 targetPos = lastHandPos + targetDirection * maxDistance;

        // ���̾ ���ư��� ����
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * shotSpeed);
            UpdateLineRenderer(); // ���� ������ ������Ʈ

            // ���� ��ġ �����
            Debug.Log("���� ��ġ: " + transform.position);

            yield return null;
        }

        // ��ǥ�� ���� �� Raycast�� ����
        hit = Physics2D.Raycast(lastHandPos, targetDirection, maxDistance, wireBuilding);
        if (hit.collider != null)
        {
            Target(); // ��ǥ�� ���̾� ����
        }
        else
        {
            // ��ü�� ������ ���̾ �ǵ���
            StartCoroutine(RetractWire(lastHandPos));
        }
    }

    private IEnumerator RetractWire(Vector3 startPos)
    {
        // ���̾ �߻� ��ġ�� �ǵ���
        while (Vector3.Distance(transform.position, startPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * retractSpeed); // �ǵ��� �ӵ�
            UpdateLineRenderer(); // ���� ������ ������Ʈ
            yield return null;
        }

        // �ǵ��ƿ��� ���̾� ��Ȱ��ȭ
        DisableWire();
    }

    private void Target()
    {
        transform.parent = hit.transform; // ��ǥ�� ���̾� ����
        Debug.Log("���̾ " + hit.collider.name + "�� �����Ǿ����ϴ�!");
    }

    private void UpdateLineRenderer()
    {
        // ���� �������� ���۰� �� ��ġ ����
        lineRenderer.SetPosition(0, playerHand.position); // �������� �÷��̾� ��
        lineRenderer.SetPosition(1, transform.position);  // ������ ���̾��� ���� ��ġ
    }
}
