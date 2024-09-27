using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    public float jumpPower;
    public int jumpCount = 0; // 점프 횟수

    public const int maxJumpCount = 2; // 최대 점프 횟수 (2단 점프)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 점프 입력 처리
        if (Input.GetMouseButtonDown(0) && jumpCount < maxJumpCount)
        {
            Jump();
        }

        // 땅에 닿았을 때 점프 횟수 초기화
        if (IsGrounded())
        {
            jumpCount = 0; // 점프 횟수 초기화
            animator.SetBool("Jumping", false); // 점프 애니메이션 종료
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower); // y축으로 점프
        jumpCount++; // 점프 횟수 증가
        animator.SetBool("Jumping", true);  // 점프 애니메이션 시작
    }

    // 플레이어가 땅에 닿았는지 확인하는 함수
    bool IsGrounded()
    {
        // 간단하게 플레이어가 땅에 있는지 속도 체크로 확인 (y축 속도가 거의 0일 때)
        return Mathf.Abs(rb.velocity.y) < 0.1f;
    }
}
