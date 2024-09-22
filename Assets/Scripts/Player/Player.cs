using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource audioSource;
    Animator animator;
    public WireController wire;

    public float jumpPower;
    private bool canJump = true; // 점프 가능 여부

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1단 점프만 가능하도록 처리
        if (Input.GetMouseButtonDown(0) && canJump)
        {
            Jump();
        }

        // 마우스 2번 클릭 시 와이어 발사
        if (Input.GetMouseButtonDown(1))
        {
            JumpShot();
        }

        // 땅에 닿았을 때 점프 가능 상태로 변경
        if (IsGrounded())
        {
            canJump = true; // 땅에 닿으면 다시 점프 가능
            animator.SetBool("Jumping", false); // 점프 애니메이션 종료
        }
    }

    public void Jump()
    {
        rb.velocity = Vector2.up * jumpPower;
        canJump = false; // 점프 후에는 다시 점프 불가 상태로 변경
        animator.SetBool("Jumping", true);  // 점프 애니메이션 시작
    }

    public void JumpShot()
    {
        wire.ShotWire(); // 와이어 발사
    }

    // 플레이어가 땅에 닿았는지 확인하는 함수
    bool IsGrounded()
    {
        // 간단하게 플레이어가 땅에 있는지 속도 체크로 확인 (y축 속도가 거의 0일 때)
        return Mathf.Abs(rb.velocity.y) < 0.1f;
    }
}
