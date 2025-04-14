using Unity.VisualScripting;
using UnityEngine;

  class Health_Base : MonoBehaviour, IDamageAble
{
    [SerializeField]
    protected float maxHealth = 100f;
    [SerializeField]
    protected float currentHealth;


    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("take " + damage);
        if (currentHealth <= 0)
            Dead();     
    }


    protected virtual void Dead()
    {
        currentHealth = 0f;
        this.gameObject.SetActive(false);
    }

}
