using UnityEditor.Media;
using UnityEngine;

public class lizardController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField]private float timeStopMoveAnim;
    private float timeAnim;

    [Header("Move")]
    [SerializeField] private float speedMove;
    [SerializeField] private float defaultSpeedMove;


    [Header("Check Player")]
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private LayerMask WhatIsPlayer;
    [SerializeField] private float playerCheckDistance;

    [Header("Flip")]
    [SerializeField] private bool facingRight = false;
    [SerializeField] private int facingDir = -1;

    [Header("Check Flip")]
    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float groundChanCheckDistance;

    [Header("Bullet")]
    [SerializeField] private Transform posBullet;
    [SerializeField] private GameObject bulletPrefabRight;
    [SerializeField] private GameObject bulletPrefabLeft;
    [SerializeField] private float timeCoolDown;


    [Header("Check Death")]
    [SerializeField] private float checkDeathDistance;

    private float timer;
    private bool isPlayer;
    private bool isGround;
    private bool isGroundChan;
    private bool isDeath;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        defaultSpeedMove = speedMove;
    }

    private void FixedUpdate()
    {
        if (isDeath)
            return;

        if (isPlayer)
        {
            rb.velocity = Vector2.zero;
            return;
        }
            

        rb.velocity = new Vector2(speedMove * facingDir,rb.velocity.y);
    }

    void Update()
    {
        if (GameObject.Find("Mario").GetComponent<Transform>().position.x > transform.position.x + 17f)
        {
            Destroy(this.gameObject);
        }
        if (isDeath)
            return;

        timer -= Time.deltaTime;
        timeAnim -= Time.deltaTime;
        RayCastCheck();
        ShootBullet();
        anim.SetBool("isMove", speedMove!=0);

        if (timeAnim > 0)
        {
            speedMove = 0;
        }
        else
        {
            speedMove = defaultSpeedMove;
        }

        if (isPlayer)
            return;

        FlipController();
    }

    private void ShootBullet()
    {
        if (timer <= 0 && isPlayer)
        {
            timer = timeCoolDown;
            anim.SetTrigger("Shooter");
        }
    }

    private void Shoot()
    {
            if (facingRight)
                Instantiate(bulletPrefabRight, posBullet.position, Quaternion.identity);
            if (!facingRight)
                Instantiate(bulletPrefabLeft, posBullet.position, Quaternion.identity);
    }

    private void RayCastCheck()
    {
        isPlayer = Physics2D.Raycast(checkPlayer.position, Vector2.left * (-facingDir), playerCheckDistance, WhatIsPlayer);
        isDeath = Physics2D.Raycast(transform.position, Vector2.up, checkDeathDistance, WhatIsPlayer);
        isGround = Physics2D.Raycast(checkGround.position, Vector2.left * (-facingDir), groundCheckDistance, WhatIsGround);
        isGroundChan = Physics2D.Raycast(checkGround.position, Vector2.down, groundCheckDistance, WhatIsGround);
    }

    void Flip()
    {
        timeAnim = timeStopMoveAnim;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir *= -1;
    }

    void FlipController()
    {
        if (!isGroundChan)
            Flip();

        if (isGround)
            Flip();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(checkPlayer.position, new Vector3(checkPlayer.position.x - playerCheckDistance * (-facingDir), checkPlayer.position.y));
        Gizmos.DrawLine(checkGround.position, new Vector3(checkGround.position.x-groundCheckDistance *(-facingDir),checkGround.position.y)); 
        Gizmos.DrawLine(checkGround.position, new Vector3(checkGround.position.x, checkGround.position.y-groundChanCheckDistance)); 
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y+checkDeathDistance)); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isDeath)
        {
            anim.SetTrigger("Death");
            //Destroy(this.gameObject,1.5f);
        }
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
