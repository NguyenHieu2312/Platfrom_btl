using UnityEngine;

[CreateAssetMenu(fileName = "SO_enemy_stat", menuName = "Scriptable Objects/SO_enemy_stat")]
public class SO_enemy_stat : ScriptableObject
{
    public string enemyName;

    public float enemyHealth;
    public float enemyDamage;
    public float enemySpeed;

    public float enemyAttackSpeed;
    public float enemyAttackRange;

    public int enemyPoint; //point when defeat enemy
}
