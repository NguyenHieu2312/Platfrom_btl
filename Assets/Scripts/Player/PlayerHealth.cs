using UnityEngine;

 class PlayerHealth : Health_Base
{
    public PlayerHealthBar playerHealthBar;

    public void Start()
    {
        if (playerHealthBar == null)
        {
            playerHealthBar = GetComponent<PlayerHealthBar>();
            if (playerHealthBar == null)
            {
                Debug.LogError("PlayerHealthBar not found!");
            }
        }
        playerHealthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        playerHealthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    protected override void Dead()
    {
        base.Dead();
        GameManager.Instance.GameOver();
        Debug.Log("Player is Dead");
    }
}
