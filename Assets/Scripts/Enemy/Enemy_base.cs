using System.Threading;
using UnityEngine;

public abstract class Enemy_Base : MonoBehaviour
{
    [SerializeField] protected SO_enemy_stat enemyManagerStat;
    public Transform player;

    [Header("Find player")]
    public float detectionRange;
    public float distanceToPlayer;

    [Header("Attack Stat")]
    protected bool isAttacking = false;
    protected float lastAttackTime;

    private SpriteRenderer spriteRenderer;
    public Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        MoveToPlayer();
        UpdateAnimation();

        if (CanAttackPlayer())
            AttackPlayer();
    }

    protected virtual void MoveToPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, LayerMask.GetMask("Player", "Ground"));
        if (hit.collider != null && !isAttacking && distanceToPlayer > enemyManagerStat.enemyAttackRange) //
        {
            if (player.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }

            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player detected!");
                transform.position = Vector2.MoveTowards(transform.position, player.position, enemyManagerStat.enemySpeed * Time.deltaTime);
                animator.SetFloat("Speed", enemyManagerStat.enemySpeed);
            }
            else
            {
                Debug.Log("Wall detected, stopping movement.");
                animator.SetFloat("Speed", 0f);
                return;
            }
        }
        //else { animator.SetFloat("Speed", 0f); }
    }

    protected abstract void AttackPlayer();

    /*protected virtual bool CanAttackPlayer()
    {
        return Time.time - lastAttackTime > enemyManagerStat.enemyAttackSpeed;
    }*/

    protected virtual bool CanAttackPlayer()
    {
        bool canAttack = Time.time - lastAttackTime > enemyManagerStat.enemyAttackSpeed && distanceToPlayer <= enemyManagerStat.enemyAttackRange; //
        if (!canAttack)
        {
            animator.SetBool("IsAttacking", false);
            isAttacking = false;
        }
        else 
        {
            animator.SetBool("IsAttacking", true);
            isAttacking = true;
        }
        return canAttack;
    }

    protected virtual void UpdateAnimation()
    {
        animator.SetFloat("Speed", (distanceToPlayer > enemyManagerStat.enemyAttackRange && !isAttacking) ? enemyManagerStat.enemySpeed : 0f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * detectionRange);
        Gizmos.DrawWireSphere(transform.position, enemyManagerStat.enemyAttackRange);
    }
}
