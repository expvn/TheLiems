using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lizardBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int Damage=1;
    [SerializeField] private float timeDestroy = 5f;
    private Rigidbody2D rb;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, timeDestroy);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            HeartController.instance.TruHP(Damage);

        Destroy(this.gameObject);
    }
}
