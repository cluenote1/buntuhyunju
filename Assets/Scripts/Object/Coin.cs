using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject coinEffect;

    private void Start()
    {
        // �ʿ��� ��� ��ü�� �� �� �� ���̵��� ��ġ�� ����
        transform.localPosition += Vector3.back * 0.1f;
    }

    private void MakeEffect()
    {
        // ��ƼŬ ȿ�� ����
        GameObject effect = Instantiate(coinEffect, transform.position, Quaternion.identity);
        effect.transform.parent = transform.parent; // �θ� ����
        effect.GetComponent<ParticleSystem>().Play();
    }

    // 2D �浹 ó���� ���� OnTriggerEnter2D�� ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �浹 ���� �α� �߰�
        Debug.Log("�浹 ����: " + other.gameObject.name);

        // �÷��̾ ���ο� �浹�ߴ��� Ȯ��
        if (other.CompareTag("Player"))
        {
            // ���� ����
            if (CompareTag("Silver"))
            {
                DataManager.Instance.UpdateScore(5);
            }
            else if (CompareTag("Gold"))
            {
                DataManager.Instance.UpdateScore(50 - (5 * DataManager.Instance.Stage));
            }

            // �Ҹ� ��� �� ȿ�� ����
            SoundManager.Instance.SFXPlay(transform.tag, clip);
            MakeEffect();

            // ���� ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
