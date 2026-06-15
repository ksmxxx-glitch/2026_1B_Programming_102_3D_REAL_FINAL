using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float rotateSpeed = 100f; // 회전 속도

    void Start()
    {
    }

    void Update()
    {
        // 아이템이 Y축을 기준으로 매 프레임마다 빙글빙글 돕니다.
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 기존 코드 유지
        Destroy(this.gameObject);
        GameController.instance.AddScore(5);
    }
}