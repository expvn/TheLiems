using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luoicua1 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed=2f;
    Vector2 move;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        move = new Vector2(0f,speed*Time.deltaTime);
        rb.velocity = move;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = -speed; 
    }
   
}
