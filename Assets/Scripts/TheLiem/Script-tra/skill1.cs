using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill1 : MonoBehaviour
{
    public GameObject target;
    public float speed =10f;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();    
        target = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = target.transform.position-transform.position;
        rb.velocity= new Vector2(direction.x,direction.y).normalized*speed;
    }

    // Update is called once per frame
    void Update()
    { 
      
        //transform.position = Vector3.MoveTowards(transform.position,target.transform.position,speed*Time.deltaTime);
        Destroy(gameObject, 2f);
    }
}
