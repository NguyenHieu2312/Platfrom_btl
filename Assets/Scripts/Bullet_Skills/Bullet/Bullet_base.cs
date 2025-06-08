using UnityEngine;

public class Bullet_base : MonoBehaviour
{
    private Transform player;
    [SerializeField] protected SO_bullet bulletData;
    private Vector2 targetPosition;
    private Vector2 directonToTarget;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = player.position;
        directonToTarget = (targetPosition - (Vector2)transform.position).normalized;
    }

    private void Update()
    {
        MoveToTarget();
        DestroyTimeLife();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            IDamageAble p_DamageAble = other.GetComponent<IDamageAble>();
            if (p_DamageAble != null)
                p_DamageAble.TakeDamage(bulletData.bulletDamage);

            Destroy(this.gameObject);
        }        
    }

    protected void MoveToTarget()
    {
        transform.Translate(directonToTarget * bulletData.bulletSpeed * Time.deltaTime, Space.World);
    }

    private void DestroyTimeLife()
    {
        Destroy(this.gameObject, bulletData.bulletLifeTime);
    }
}
