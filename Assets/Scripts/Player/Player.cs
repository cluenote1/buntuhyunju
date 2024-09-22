using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource audioSource;
    Animator animator;   // Animator �߰�

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
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jumping", true);  // ���� �ִϸ��̼� ����
        }

        // �÷��̾ ���� ����� �� �ִϸ��̼� ���¸� ������� ����
        if (rb.velocity.y == 0)
        {
            animator.SetBool("Jumping", false); // ���� �ִϸ��̼� ����
        }
    }
}
