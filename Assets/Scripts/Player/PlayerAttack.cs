using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float atkDamge;
    [SerializeField] private float atkRange;
    [SerializeField] private float atkCD;
    [SerializeField] private Transform atkPoint;
    [SerializeField] private LayerMask enemyLayer;

    private float lastAtkTime;


    void Start()
    {
        // if no poit transform, now transform is player
        if (atkPoint is null) 
            atkPoint = transform;
    }

    void Update()
    {
        // left click to atk
        if (Input.GetMouseButtonDown(0) && CanAttack())
            Attack();
        
    }

    bool CanAttack()
    {
        //if CD, can atk
        return Time.time - lastAtkTime > atkCD; 
    }

    void Attack()
    {
        lastAtkTime = Time.time;
        // use a cirle to check enmin range
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(
            atkPoint.position,      //now is player pos
            atkRange,               // range can atk
            enemyLayer              // layer in unity(not sorting layer)
        );

        foreach (Collider2D enemy in enemyInRange)
        {
            IDamageAble iDamange = enemy.GetComponent<IDamageAble>();

            if (iDamange != null)
                iDamange.TakeDamage(atkDamge); // dmg here!
            
        }
    }

    void OnDrawGizmos()
    {
        if (atkPoint != null) 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(atkPoint.position, atkRange);
        }
    }
}
