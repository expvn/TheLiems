using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private GameObject poligon;
    [Header("Move")]
    [SerializeField] private float speedMove;

    [Header("Flip")]
    [SerializeField] private bool facingRight = true;
    [SerializeField] private int facingDir = 1;
    [SerializeField] private float timeStopMoveAnim;
    private float timeAnim;

    [Header("Check Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private float groungCheckDirection;
    [SerializeField] private bool isGround;
    [SerializeField] private bool isGroundX;

    [Header("Check Player")]
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private LayerMask WhatIsPlayer;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private bool isPlayer;

    [Header("see player")]
    [SerializeField] private bool seePlayer;
    [SerializeField] private float seePlayerDistance = 10f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool daDanh;//da danh = true dung khoang 2s
    [SerializeField] private float timeCoolDownAttack = 2f;
    [SerializeField] private float timerAttack;

    [Header("Death")]
    [SerializeField] private bool isDeath;
    [SerializeField] private float deathDistance = .35f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (isDeath)
        {
            return;
        }
        if (timeAnim > 0||timerAttack>0)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = new Vector2(speedMove * facingDir, rb.velocity.y);
    }
    void Update()
    {
        timerAttack -= Time.deltaTime;
        if (isDeath)
        {
            return;
        }
        if (isAttack && timerAttack <= 0)
        {
            Attack();
            timerAttack = timeCoolDownAttack;
        }
        timeAnim -= Time.deltaTime;
        RayCastCheck();
        anim.SetBool("SpinBool", rb.velocity.x != 0);

        if (isPlayer)
        {
            timeAnim = -1;
            return;
        }

        FlipController();
    }

    private void RayCastCheck()
    {
        isGround = Physics2D.Raycast(groundCheck.position, Vector2.down * groungCheckDirection, WhatIsGround);
        isGroundX = Physics2D.Raycast(groundCheck.position, Vector2.right * groungCheckDirection * facingDir, WhatIsGround);

        isPlayer = Physics2D.Raycast(checkPlayer.position, Vector2.left * (-facingDir), playerCheckDistance, WhatIsPlayer);
        isAttack = Physics2D.Raycast(checkPlayer.position, Vector2.left * (-facingDir), attackDistance, WhatIsPlayer);

        isDeath = Physics2D.Raycast(transform.position, Vector2.up, attackDistance, WhatIsPlayer);

        if (isDeath)
        {
            anim.SetTrigger("Death");
            poligon.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groungCheckDirection));
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x + groungCheckDirection * facingDir, groundCheck.position.y));

        Gizmos.DrawLine(checkPlayer.position, new Vector3(checkPlayer.position.x - playerCheckDistance * (-facingDir), checkPlayer.position.y));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(checkPlayer.position, new Vector3(checkPlayer.position.x - attackDistance * (-facingDir), checkPlayer.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + deathDistance));
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
        if (isGroundX)
            Flip();

        if (!isGround)
            Flip();
    }

    void Attack()
    {
        HeartController.instance.TruHP(1);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
