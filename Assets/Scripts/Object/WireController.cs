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
    [SerializeField] private SpriteRenderer wireSprite; // 와이어 스프라이트 렌더러

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
        // 와이어 스프라이트의 위치를 플레이어의 손 위치와 동기화
        if (isWire)
        {
            wireSprite.transform.position = (playerHand.position + transform.position) / 2;
            wireSprite.size = new Vector2(Vector3.Distance(playerHand.position, transform.position), wireSprite.size.y);

            // 회전 설정
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
        wireSprite.enabled = true;  // 와이어 스프라이트 활성화
        StartCoroutine("Shot");
    }

    public void DisableWire()
    {
        onWire = false;
        isWire = false;
        wireSprite.enabled = false; // 와이어 스프라이트 비활성화
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

            // 스프라이트 크기 조정 (와이어가 길어지거나 줄어드는 효과)
            wireSprite.size = new Vector2(wireDistance, wireSprite.size.y);

            // 2D Raycast 사용
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
        transform.parent = hit.transform; // 2D에서 부모로 설정
        onWire = true;
    }
}
