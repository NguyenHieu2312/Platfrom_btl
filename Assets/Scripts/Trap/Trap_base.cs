using UnityEngine;
using System.Collections;

public class Trap_base : MonoBehaviour
{
    [SerializeField] private float trapDamage = 10f;        
    [SerializeField] private float trapDmgInterval = 0.5f;  
    private PlayerHealth playerHealth;
    private bool isPlayerInTrap = false;                    
    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(trapDamage); 
                Debug.Log("1st dmg");
                isPlayerInTrap = true;              
                damageCoroutine = StartCoroutine(DamageOverTime()); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInTrap = false;             
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine); 
                damageCoroutine = null;
            }
            playerHealth = null;
        }
    }
 
    private IEnumerator DamageOverTime()
    {
        while (isPlayerInTrap && playerHealth != null) 
        {
            yield return new WaitForSeconds(trapDmgInterval); 
            if (isPlayerInTrap && playerHealth != null)       
            {
                playerHealth.TakeDamage(trapDamage);
                Debug.Log("stay dmg");
            }
        }
    }
}