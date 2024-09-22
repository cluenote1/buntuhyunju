using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField] private Transform playerHand;
    [SerializeField] private Vector3 shotPos;
    [SerializeField] private float shotSpeed = 10f;
    [SerializeField] private float maxDistance = 9f;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private LayerMask wireBuilding;

    private float wireDistance;
    public float WireDistance => wireDistance;

    private bool onWire;
    public bool OnWire => onWire;

    private LineRenderer line;
    private RaycastHit2D hit; // 2D RaycastHit
    private bool isWire;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        DisableWire();
    }

    private void Update()
    {
        line.SetPosition(0, playerHand.position);
        line.SetPosition(1, transform.position);
    }

    public void ShotWire()
    {
        if (isWire) return;
        SoundManager.Instance.SFXPlay("ShotWire", shotSound);
        isWire = true;
        line.enabled = true;
        StartCoroutine("Shot");
    }

    public void DisableWire()
    {
        onWire = false;
        isWire = false;
        line.enabled = false;
        transform.parent = null;
    }

    private IEnumerator Shot()
    {
        Vector3 lastHandPos = playerHand.position;
        transform.position = lastHandPos;
        Vector3 targetPos = (shotPos - lastHandPos).normalized * maxDistance;
        wireDistance = Vector3.Distance(transform.position, targetPos + lastHandPos);

        while (wireDistance > 0f)
        {
            wireDistance = Vector3.Distance(transform.position, targetPos + lastHandPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPos + lastHandPos, Time.deltaTime * shotSpeed);

            // 2D Raycast 사용
            hit = Physics2D.Raycast(lastHandPos, (targetPos - lastHandPos).normalized, maxDistance - wireDistance, wireBuilding);
            if (hit.collider != null)
            {
                Target();
                break;
            }
            yield return null;
        }
        if (wireDistance == 0f)
            DisableWire();
    }

    private void Target()
    {
        transform.parent = hit.transform; // 2D에서 부모로 설정
        onWire = true;
    }
}
