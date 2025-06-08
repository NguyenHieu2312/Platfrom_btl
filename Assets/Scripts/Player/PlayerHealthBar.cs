using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealthBar : HealthBar
{
    public Image fillWhenTakeDamageBar;
    public Image fillLater;
    public float minusHelthSpeed;
    private void Update()
    {

        if (fillLater.fillAmount > fillWhenTakeDamageBar.fillAmount)
        {
            fillLater.fillAmount -= minusHelthSpeed;
        }
        else
        {
            fillLater.fillAmount = fillWhenTakeDamageBar.fillAmount;
        }
    }
    public override void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        fillWhenTakeDamageBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0 ) { fillWhenTakeDamageBar.fillAmount = 0; }
        //_healthToText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
