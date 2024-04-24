using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Boss1attack : MonoBehaviour
{
    public GameObject skill1;
    public GameObject skill2;
    public float range=11f;
    public Transform target;
    float distance;
   public Animator animator;
    public float mintimeNextskill = 0f;
    public float maxtimeNextskill = 0.5f;
    private float timeUntilnextskill;
    private bool canUseskill = true;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timeUntilnextskill = getRadomTime();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(canUseskill)
        {
            timeUntilnextskill -= Time.deltaTime;
        }
        if(timeUntilnextskill <= 0 ) {
            Radomskill();
            timeUntilnextskill = getRadomTime();
        
        
        
        }
        Attack();
    }
    public void Attack()
    {
             distance = Mathf.Abs(Vector2.Distance(transform.position,target.position ));
        Debug.Log(distance);
        if (distance < range)
        {
          
            Debug.Log("chuẩn bị ăn đòn");
        }
    }
    private float getRadomTime()   // radom thoi gian ra chieu
    {
        return Random.Range(mintimeNextskill, maxtimeNextskill);

    }
    private void Radomskill()  // radom skill.
    {
        Vector3 transformskill = target.transform.position + new Vector3(5f, -1f,0f);
        float radomskil = Random.Range(1, 2);
        switch(radomskil)
        {
            case 1:
                animator.SetTrigger("attack1");
              
                Instantiate(skill1,transform.position, Quaternion.identity);
                
                break;
            case 2:
                animator.SetTrigger("attack2");
             
                Instantiate(skill2,transformskill, Quaternion.identity);
                break;

        }
    }

}
