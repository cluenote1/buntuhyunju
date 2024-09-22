using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource audioSource;
    Animator animator;   // Animator �߰�
    public WireController wire; // WireController �߰�

    public float jumpPower;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();  // Animator ������Ʈ �Ҵ�
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // ���̾� �߻�
        if (Input.GetMouseButtonDown(1)) // ��Ŭ�� �Ǵ� �ٸ� ��ư���� ���� ����
        {
            JumpShot();
        }

        // �÷��̾ ���� ����� �� �ִϸ��̼� ���¸� ������� ����
        if (rb.velocity.y == 0)
        {
            animator.SetBool("Jumping", false); // ���� �ִϸ��̼� ����
        }
    }

    public void Jump()
    {
        rb.velocity = Vector2.up * jumpPower;
        animator.SetBool("Jumping", true);  // ���� �ִϸ��̼� ����
    }

    public void JumpShot()
    {
        wire.ShotWire(); // ���̾� �߻�
    }
}
