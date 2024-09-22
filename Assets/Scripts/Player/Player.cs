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
    private bool canJump = true; // ���� ���� ����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1�� ������ �����ϵ��� ó��
        if (Input.GetMouseButtonDown(0) && canJump)
        {
            Jump();
        }

        // ���콺 2�� Ŭ�� �� ���̾� �߻�
        if (Input.GetMouseButtonDown(1))
        {
            JumpShot();
        }

        // ���� ����� �� ���� ���� ���·� ����
        if (IsGrounded())
        {
            canJump = true; // ���� ������ �ٽ� ���� ����
            animator.SetBool("Jumping", false); // ���� �ִϸ��̼� ����
        }
    }

    public void Jump()
    {
        rb.velocity = Vector2.up * jumpPower;
        canJump = false; // ���� �Ŀ��� �ٽ� ���� �Ұ� ���·� ����
        animator.SetBool("Jumping", true);  // ���� �ִϸ��̼� ����
    }

    public void JumpShot()
    {
        wire.ShotWire(); // ���̾� �߻�
    }

    // �÷��̾ ���� ��Ҵ��� Ȯ���ϴ� �Լ�
    bool IsGrounded()
    {
        // �����ϰ� �÷��̾ ���� �ִ��� �ӵ� üũ�� Ȯ�� (y�� �ӵ��� ���� 0�� ��)
        return Mathf.Abs(rb.velocity.y) < 0.1f;
    }
}
