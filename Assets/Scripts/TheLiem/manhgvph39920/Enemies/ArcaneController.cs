using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ArcaneController : MonoBehaviour
{


    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float timeStopMoveAnim;
    private float timeAnim;

    [Header("Speed")]
    [SerializeField] private float speed;

    [Header("Check Ground")]
    [SerializeField] private Transform checkGround;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isGround;

    [Header("Facing Right")]
    [SerializeField] private bool facingRight = true;
    [SerializeField] private int facingDir = 1;

    [Header("Bullet")]
    [SerializeField] private Transform posBullet;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timer;
    [SerializeField] private float timeCoolDown = 3f;
    [SerializeField] private bool checkShoot;
    [SerializeField] private float distanceSeePlayer;


    Player player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Mario").GetComponent<Player>();
        checkShoot = false;
    }
    private void FixedUpdate()
    {
        if (timeAnim > 0 || checkShoot)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = new Vector2(speed * facingDir, rb.velocity.y);
    }
    void Update()
    {
        if (player.transform.position.x > transform.position.x + 17f)
        {
            Destroy(this.gameObject);
        }
        timeAnim -= Time.deltaTime;
        
        checkPlayerAndShoot();
        CheckRaycast();
        anim.SetBool("Run",rb.velocity.x != 0);

        if (checkShoot)
            return;
        FlipController();
    }
    private void CheckRaycast()
    {
        isGround = Physics2D.Raycast(checkGround.position, Vector2.down * groundCheckDistance, WhatIsGround);
    }
    private void checkPlayerAndShoot()
    {
        if (timeAnim <= 0)
        {
            timeAnim = timeCoolDown;
            if (Vector3.Distance(player.transform.position, transform.position) < distanceSeePlayer)
            {
                if (player.transform.position.x < 0&&facingRight)
                {
                    Flip();
                }else if (player.transform.position.x > 0&&!facingRight)
                {
                    Flip();
                }
                if (player.transform.position.x < transform.position.x && !facingRight)
                {
                    anim.SetTrigger("attack");
                }
                else if (player.transform.position.x > transform.position.x && facingRight)
                {
                    anim.SetTrigger("attack");
                }
                else
                {
                    checkShoot = false;
                }
            }
        }
    }
    public void Shoot()
    {
        checkShoot = true;
        Vector3 direction = posBullet.position - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        bulletPrefab.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Instantiate(bulletPrefab, posBullet.transform.position, bulletPrefab.transform.rotation);
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
        if (!isGround)
            Flip();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(checkGround.position,new Vector3(checkGround.position.x,checkGround.position.y-groundCheckDistance, 0));
    }
}
