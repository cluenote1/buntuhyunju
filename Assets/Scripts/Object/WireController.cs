using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField] private Transform playerHand;
    [SerializeField] private Vector3 shotPos;
    [SerializeField] private float shotSpeed = 10f;
    [SerializeField] private float maxDistance = 9f;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private LayerMask wireBuilding;
    [SerializeField] private SpriteRenderer wireSprite; // ���̾� ��������Ʈ ������

    private float wireDistance;
    public float WireDistance => wireDistance;

    private bool onWire;
    public bool OnWire => onWire;

    private RaycastHit2D hit; // 2D RaycastHit
    private bool isWire;

    private void Start()
    {
        DisableWire();
    }

    private void Update()
    {
        // ���̾� ��������Ʈ�� ��ġ�� �÷��̾��� �� ��ġ�� ����ȭ
        if (isWire)
        {
            wireSprite.transform.position = (playerHand.position + transform.position) / 2;
            wireSprite.size = new Vector2(Vector3.Distance(playerHand.position, transform.position), wireSprite.size.y);

            // ȸ�� ����
            Vector3 direction = (transform.position - playerHand.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            wireSprite.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void ShotWire()
    {
        if (isWire) return;
        SoundManager.Instance.SFXPlay("ShotWire", shotSound);
        isWire = true;
        wireSprite.enabled = true;  // ���̾� ��������Ʈ Ȱ��ȭ
        StartCoroutine("Shot");
    }

    public void DisableWire()
    {
        onWire = false;
        isWire = false;
        wireSprite.enabled = false; // ���̾� ��������Ʈ ��Ȱ��ȭ
        transform.parent = null;
    }

    private IEnumerator Shot()
    {
        Vector3 lastHandPos = playerHand.position;
        transform.position = lastHandPos;
        Vector3 targetPos = (shotPos - lastHandPos).normalized * maxDistance;
        wireDistance = Vector3.Distance(transform.position, targetPos + lastHandPos);

        while (wireDistance > 0f)
        {
            wireDistance = Vector3.Distance(transform.position, targetPos + lastHandPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPos + lastHandPos, Time.deltaTime * shotSpeed);

            // ��������Ʈ ũ�� ���� (���̾ ������ų� �پ��� ȿ��)
            wireSprite.size = new Vector2(wireDistance, wireSprite.size.y);

            // 2D Raycast ���
            hit = Physics2D.Raycast(lastHandPos, (targetPos - lastHandPos).normalized, maxDistance - wireDistance, wireBuilding);
            if (hit.collider != null)
            {
                Target();
                break;
            }
            yield return null;
        }

        if (wireDistance == 0f)
            DisableWire();
    }

    private void Target()
    {
        transform.parent = hit.transform; // 2D���� �θ�� ����
        onWire = true;
    }
}
