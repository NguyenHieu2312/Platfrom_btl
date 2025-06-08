using UnityEngine;

public class Enemy_long_range : Enemy_Base
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    // my plan is to make enemy chasing wwith player, but it too hard :> they fall and dead -.-

    //[SerializeField] private float chasingRange = 2f;

    /*    protected override void MoveToPlayer()
        {
            base.MoveToPlayer();
            Vector2 direction = (player.position - transform.position).normalized;
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= chasingRange)
            {
                transform.Translate(-direction * enemyManagerStat.enemySpeed * Time.deltaTime);
            }
        }*/
    override protected void AttackPlayer()
    {
        lastAttackTime = Time.time;
        /*Vector2 directionToPLayer = (player.position - transform.position).normalized;
        RaycastHit2D attackHit = Physics2D.Raycast(this.transform.position, 
            directionToPLayer, enemyManagerStat.enemyAttackRange, LayerMask.GetMask("Player"));

        if (attackHit.collider != null)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }*/

        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= enemyManagerStat.enemyAttackRange)
        {
            isAttacking = true;
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Debug.Log("Attacking player!");
        }
        else
        {
            isAttacking = false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * enemyManagerStat.enemyAttackRange);
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * detectionRange);
        Gizmos.DrawWireSphere(transform.position, enemyManagerStat.enemyAttackRange);
    }
}
