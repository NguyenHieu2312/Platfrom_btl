using UnityEngine;

[CreateAssetMenu(fileName = "SO_bullet", menuName = "Scriptable Objects/SO_bullet")]
public class SO_bullet : ScriptableObject
{
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletLifeTime;
}
