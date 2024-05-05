using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxxoay : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = -200f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();



    }

    void Update()
    {
        // Cho thanh ngang quay theo chiều kim đồng hồ với tốc độ 100 đơn vị mỗi giây
        Quaternion quay = Quaternion.Euler(0f, 0f, speed * Time.deltaTime);
        transform.localRotation *= quay;

    }
}
