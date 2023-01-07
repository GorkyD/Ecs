using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    [Header("Player")][Space]
    public float playerSpeed;
    public float playerHealth;
    
    [Space][Header("Enemy")][Space]
    public float enemySpeed;
    public float enemyHealth;
    public float distanceToStop;
}