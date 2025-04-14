using System.Collections;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;

    [Header("Move Settings")]
    [SerializeField] private float playerSpeed = 5f;
    float horizontalMove;


    [Header("Jump Settings")]
    [SerializeField] private float playerJumpForce = 5f;
    private bool isGrounded;
    private int jumpCount;
    //[SerializeField]
    //private int maxJumpTimes = 2;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool canDoubleJump;
    //private bool isFirstJump;

    [Header("Dash Settings")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private TrailRenderer dashTrail;

    [Header("Flip")]
    private bool isFacingRight = true;

    [SerializeField] private GameObject startPos;
    private Animator anim;

    private void Awake()
    {
        this.transform.position = startPos.transform.position;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        jumpCount = 0;
    }

    void Update()
    {
        if (isDashing)
            return;


        if (IsGrounded() && !Input.GetButton("Jump"))//if player on the grd and not jump1, can't dbJump
            canDoubleJump = false;

        PlayerMove();
        PlayerJump();   
        Flip();


        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash());
        //Debug
        Debug.Log("jumpcount" + jumpCount);
        Debug.Log("dbj" + canDoubleJump);
        Debug.Log("isGRd" + IsGrounded());
        Debug.Log("candash " + canDash);
    }

    void PlayerMove()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal"); // move x :>
        playerRb.linearVelocity = new Vector2 (horizontalMove * playerSpeed, playerRb.linearVelocity.y);

        if (!Input.GetButton("Jump") && IsGrounded())
        {
            anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
    }

     bool IsGrounded()// check player on the ground
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundLayer);// circle to check ground layer
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || canDoubleJump)// can jump when jump1 or jump2
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocityX, playerJumpForce);
                canDoubleJump = !canDoubleJump;
                //jumpCount++;
                anim.SetBool("IsDashing", true);
            }
        }

        if (IsGrounded() && playerRb.linearVelocity.y <= 0)
        {
            anim.SetBool("IsDashing", false);
        }
    }
    
    void Flip()
    {
        if (isFacingRight && horizontalMove < 0f || !isFacingRight && horizontalMove > 0f) //default asset is facing right, can rotate in derection. hrzInput check a vs d
        {
            Vector3 playerLocalScale = transform.localScale;
            isFacingRight = !isFacingRight;
            playerLocalScale.x *= -1f; // rotate * -1 in direction
            transform.localScale = playerLocalScale;
        }
    }

    private IEnumerator Dash() // to use coroutine
    {
        canDash = false;
        isDashing = true;
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        playerRb.linearVelocity = new Vector2(transform.localScale.x * dashForce, 0f);
        dashTrail.emitting = true;
        anim.SetBool("IsJumping", true);
        yield return new WaitForSeconds(dashTime);// dashing

        dashTrail.emitting = false;
        playerRb.gravityScale = originalGravity;
        isDashing = false;
        anim.SetBool("IsJumping", false);
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }
}
