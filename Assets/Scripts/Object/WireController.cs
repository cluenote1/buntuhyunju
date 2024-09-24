using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField] private Transform playerHand; // 플레이어 손 위치
    [SerializeField] private float shotSpeed = 10f; // 발사 속도
    [SerializeField] private float maxDistance = 9f; // 최대 거리
    [SerializeField] private AudioClip shotSound; // 발사 소리
    [SerializeField] private LayerMask wireBuilding; // 와이어가 연결될 빌딩 레이어
    [SerializeField] private LineRenderer lineRenderer; // 라인 렌더러

    private bool isWire; // 현재 와이어 상태
    private RaycastHit2D hit; // 2D RaycastHit

    private float lastClickTime; // 마지막 클릭 시간
    private int clickCount; // 클릭 카운트
    private float clickDelay = 0.3f; // 두 번 클릭 간격

    private void Start()
    {
        DisableWire();
    }

    private void Update()
    {
        // 좌클릭 감지
        if (Input.GetMouseButtonDown(0)) // 0은 좌클릭
        {
            if (Time.time - lastClickTime <= clickDelay) // 두 번 클릭이 간격 내에 있는지 확인
            {
                clickCount++;
                if (clickCount == 2) // 두 번 클릭
                {
                    ShotWire(); // 와이어 발사
                    clickCount = 0; // 카운트 초기화
                }
            }
            else
            {
                clickCount = 1; // 카운트 초기화 후 1로 설정
            }
            lastClickTime = Time.time; // 마지막 클릭 시간 업데이트
        }

        // 와이어 스프라이트의 위치를 플레이어의 손 위치와 동기화
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
        lineRenderer.enabled = true; // 라인 렌더러 활성화
        StartCoroutine("Shot");
    }

    public void DisableWire()
    {
        isWire = false;
        lineRenderer.enabled = false; // 라인 렌더러 비활성화
        transform.parent = null;
    }

    private IEnumerator Shot()
    {
        Vector3 lastHandPos = playerHand.position;
        Vector3 targetPos = lastHandPos + (Vector3.right * maxDistance); // 원하는 방향으로 설정, 여기서 Vector3.right을 원하는 방향으로 바꿀 수 있음

        // 와이어가 날아가는 동안
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * shotSpeed);
            yield return null;
        }

        // 목표에 도달 시 Raycast를 수행
        hit = Physics2D.Raycast(lastHandPos, (targetPos - lastHandPos).normalized, maxDistance, wireBuilding);
        if (hit.collider != null)
        {
            Target();
        }
        DisableWire(); // 와이어 비활성화
    }

    private void Target()
    {
        transform.parent = hit.transform; // 2D에서 부모로 설정
    }

    private void UpdateLineRenderer()
    {
        // 라인 렌더러의 시작과 끝 위치 설정
        lineRenderer.SetPosition(0, playerHand.position);
        lineRenderer.SetPosition(1, transform.position);
    }
}
