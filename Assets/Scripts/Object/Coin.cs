using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject coinEffect;

    private void Start()
    {
        // 필요할 경우 물체가 좀 더 잘 보이도록 위치를 조정
        transform.localPosition += Vector3.back * 0.1f;
    }

    private void MakeEffect()
    {
        // 파티클 효과 생성
        GameObject effect = Instantiate(coinEffect, transform.position, Quaternion.identity);
        effect.transform.parent = transform.parent; // 부모 설정
        effect.GetComponent<ParticleSystem>().Play();
    }

    // 2D 충돌 처리를 위해 OnTriggerEnter2D로 수정
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌 감지 로그 추가
        Debug.Log("충돌 감지: " + other.gameObject.name);

        // 플레이어가 코인에 충돌했는지 확인
        if (other.CompareTag("Player"))
        {
            // 점수 증가
            if (CompareTag("Silver"))
            {
                DataManager.Instance.UpdateScore(5);
            }
            else if (CompareTag("Gold"))
            {
                DataManager.Instance.UpdateScore(50 - (5 * DataManager.Instance.Stage));
            }

            // 소리 재생 및 효과 생성
            SoundManager.Instance.SFXPlay(transform.tag, clip);
            MakeEffect();

            // 코인 비활성화
            gameObject.SetActive(false);
        }
    }
}
