using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeDestroy=5f;
    private void Start()
    {
        Destroy(this.gameObject, timeDestroy);
    }
    private void FixedUpdate()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HeartController.instance.TruHP(1);
            Destroy(this.gameObject);
        }
    }
}
