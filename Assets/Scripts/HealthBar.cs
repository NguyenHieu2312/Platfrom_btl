using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    public abstract void UpdateHealthBar(float currentHealth, float maxHealth);
}
