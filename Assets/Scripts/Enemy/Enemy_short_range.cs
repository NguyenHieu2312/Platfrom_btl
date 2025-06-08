using UnityEngine;

public class Enemy_short_range : Enemy_Base
{
    override protected void AttackPlayer()
    {
        lastAttackTime = Time.time;
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Collider2D playerCollider = Physics2D.OverlapCircle(this.transform.position, enemyManagerStat.enemyAttackRange, LayerMask.GetMask("Player"));
        if (distanceToPlayer <= enemyManagerStat.enemyAttackRange)
        {
            isAttacking = true;
            IDamageAble p_DamageAble = playerCollider.GetComponent<IDamageAble>();
            if (p_DamageAble != null)
            {
                p_DamageAble.TakeDamage(enemyManagerStat.enemyDamage);
            }
            Debug.Log("Attacking player!");
        }
        else
        {
            isAttacking = false;
        }
    }
}
