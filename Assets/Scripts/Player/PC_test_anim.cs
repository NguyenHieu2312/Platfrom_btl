using UnityEngine;
using System.Collections;

public class PC_test_anim : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator anim;

    [Header("Move Settings")]
    [SerializeField] private float playerSpeed = 5f;
    private float horizontalMove;

    [Header("Jump Settings")]
    [SerializeField] private float playerJumpForce = 5f;
    private bool isGrounded;
    private bool canDoubleJump;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

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

    private void Awake()
    {
        transform.position = startPos.transform.position;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
            return;

        isGrounded = IsGrounded();
        if (isGrounded && !Input.GetButton("Jump"))
        {
            canDoubleJump = false;
        }

        UpdateAnimation();

        PlayerMove();
        PlayerJump();
        Flip();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }


        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("IsAttacking", true);
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }

        Debug.Log("isGrounded: " + isGrounded);
        Debug.Log("canDoubleJump: " + canDoubleJump);
        Debug.Log("canDash: " + canDash);
    }

    void PlayerMove()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        playerRb.linearVelocity = new Vector2(horizontalMove * playerSpeed, playerRb.linearVelocity.y);
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, groundLayer);
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || canDoubleJump)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, playerJumpForce);
                canDoubleJump = !canDoubleJump;
                anim.SetBool("IsJumping", true);
            }
        }

        if (isGrounded && playerRb.linearVelocity.y <= 0)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", false);
        }
        else if (!isGrounded && playerRb.linearVelocity.y < 0)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", true);
        }
    }

    void Flip()
    {
        if (isFacingRight && horizontalMove < 0f || !isFacingRight && horizontalMove > 0f)
        {
            Vector3 playerLocalScale = transform.localScale;
            isFacingRight = !isFacingRight;
            playerLocalScale.x *= -1f;
            transform.localScale = playerLocalScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        anim.SetBool("IsDashing", true);
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        playerRb.linearVelocity = new Vector2(transform.localScale.x * dashForce, 0f);
        dashTrail.emitting = true;

        yield return new WaitForSeconds(dashTime);

        dashTrail.emitting = false;
        playerRb.gravityScale = originalGravity;
        isDashing = false;
        anim.SetBool("IsDashing", false);
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    void UpdateAnimation()
    {
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPos.position, groundCheckRadius);
    }
}
