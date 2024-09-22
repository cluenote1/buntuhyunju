using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource audioSource;
    Animator animator;   // Animator 추가
    public WireController wire; // WireController 추가

    public float jumpPower;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();  // Animator 컴포넌트 할당
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // 와이어 발사
        if (Input.GetMouseButtonDown(1)) // 우클릭 또는 다른 버튼으로 설정 가능
        {
            JumpShot();
        }

        // 플레이어가 땅에 닿았을 때 애니메이션 상태를 원래대로 변경
        if (rb.velocity.y == 0)
        {
            animator.SetBool("Jumping", false); // 점프 애니메이션 종료
        }
    }

    public void Jump()
    {
        rb.velocity = Vector2.up * jumpPower;
        animator.SetBool("Jumping", true);  // 점프 애니메이션 시작
    }

    public void JumpShot()
    {
        wire.ShotWire(); // 와이어 발사
    }
}
