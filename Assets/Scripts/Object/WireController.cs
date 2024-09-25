using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField] private Transform playerHand; // 플레이어 손 위치
    [SerializeField] private float shotSpeed = 10f; // 발사 속도
    [SerializeField] private float retractSpeed = 15f; // 되돌림 속도 (기본값으로 더 빠르게 설정)
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
        // 라인 렌더러 초기 설정
        lineRenderer.startWidth = 0.5f; // 시작 두께
        lineRenderer.endWidth = 0.5f; // 끝 두께
        lineRenderer.startColor = Color.red; // 시작 색상
        lineRenderer.endColor = Color.red; // 끝 색상

        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 재질 설정
        transform.position = playerHand.position; // 초기 위치 설정
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

        // 와이어 상태에 따라 위치 업데이트
        if (isWire)
        {
            UpdateLineRenderer();
        }
        else
        {
            // 와이어가 비활성화된 경우에도 플레이어 손 위치를 따라가도록 업데이트
            if (playerHand != null)
            {
                transform.position = playerHand.position; // 와이어 위치를 플레이어 손 위치로 업데이트
            }
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

        // 2시 방향으로 목표 설정
        Vector3 targetDirection = new Vector3(1, 1, 0).normalized;
        Vector3 targetPos = lastHandPos + targetDirection * maxDistance;

        // 와이어가 날아가는 동안
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * shotSpeed);
            UpdateLineRenderer(); // 라인 렌더러 업데이트

            // 현재 위치 디버그
            Debug.Log("현재 위치: " + transform.position);

            yield return null;
        }

        // 목표에 도달 시 Raycast를 수행
        hit = Physics2D.Raycast(lastHandPos, targetDirection, maxDistance, wireBuilding);
        if (hit.collider != null)
        {
            Target(); // 목표에 와이어 고정
        }
        else
        {
            // 물체가 없으면 와이어를 되돌림
            StartCoroutine(RetractWire(lastHandPos));
        }
    }

    private IEnumerator RetractWire(Vector3 startPos)
    {
        // 와이어를 발사 위치로 되돌림
        while (Vector3.Distance(transform.position, startPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * retractSpeed); // 되돌림 속도
            UpdateLineRenderer(); // 라인 렌더러 업데이트
            yield return null;
        }

        // 되돌아오면 와이어 비활성화
        DisableWire();
    }

    private void Target()
    {
        transform.parent = hit.transform; // 목표에 와이어 고정
        Debug.Log("와이어가 " + hit.collider.name + "에 고정되었습니다!");
    }

    private void UpdateLineRenderer()
    {
        // 라인 렌더러의 시작과 끝 위치 설정
        lineRenderer.SetPosition(0, playerHand.position); // 시작점은 플레이어 손
        lineRenderer.SetPosition(1, transform.position);  // 끝점은 와이어의 현재 위치
    }
}
