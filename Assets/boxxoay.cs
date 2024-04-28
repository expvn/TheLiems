using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxxoay : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
       rb.AddTorque(100f*Time.deltaTime);
    }
}
