using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cucgachcuoi : MonoBehaviour
{
    private float speed = 500f;
    private Rigidbody2D rb;
    private bool xoay = false;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();    
    }


   
    // Update is called once per frame
    void Update()
    {
        if (xoay == true)
        {
            Quaternion xoay = Quaternion.Euler(0f, 0f, 200f*Time.deltaTime);
            transform.localRotation *= xoay;
        }
    }
   void OnTriggerEnter2D(Collider2D other)
    {
       // if(other.CompareTag)
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           xoay = true;
        }
    }
}
