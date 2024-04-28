using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss1 : MonoBehaviour
{   
    private Animator animator;
    public Transform player;
    private Rigidbody2D rb;
    public float speed =5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb= gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { //Vector2 move = new Vector2(speed*Time.deltaTime,0f);
        //rb.velocity = move;

        Vector2 target = new Vector2( player.position.x,rb.position.y );
       transform.position= Vector2.MoveTowards(rb.position, target,speed *Time.deltaTime);
       
    }



}
