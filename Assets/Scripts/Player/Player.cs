using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    public float jumpPower;
    public int jumpCount = 0; // ���� Ƚ��

    public const int maxJumpCount = 2; // �ִ� ���� Ƚ�� (2�� ����)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ���� �Է� ó��
        if (Input.GetMouseButtonDown(0) && jumpCount < maxJumpCount)
        {
            Jump();
        }

        // ���� ����� �� ���� Ƚ�� �ʱ�ȭ
        if (IsGrounded())
        {
            jumpCount = 0; // ���� Ƚ�� �ʱ�ȭ
            animator.SetBool("Jumping", false); // ���� �ִϸ��̼� ����
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower); // y������ ����
        jumpCount++; // ���� Ƚ�� ����
        animator.SetBool("Jumping", true);  // ���� �ִϸ��̼� ����
    }

    // �÷��̾ ���� ��Ҵ��� Ȯ���ϴ� �Լ�
    bool IsGrounded()
    {
        // �����ϰ� �÷��̾ ���� �ִ��� �ӵ� üũ�� Ȯ�� (y�� �ӵ��� ���� 0�� ��)
        return Mathf.Abs(rb.velocity.y) < 0.1f;
    }
}
