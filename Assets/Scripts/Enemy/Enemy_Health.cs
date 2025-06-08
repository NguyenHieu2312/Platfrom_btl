using UnityEngine;

 class Enemy_Health : Health_Base
{
    [SerializeField] private SO_enemy_stat enemyManagerStat;
    protected override void Awake()
    {
        maxHealth = enemyManagerStat.enemyHealth;
        base.Awake();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Dead()
    {
        base.Dead();
        ScoreManager.Instance.AddScore(enemyManagerStat.enemyPoint);
    }
}
