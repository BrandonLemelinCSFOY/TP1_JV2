using UnityEngine;

// TODO : Ajoutez toutes les références à vos ObjectPools ici.
//        Basez-vous sur le code existant.
public class ObjectPools : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private ObjectPool alien;

    [Header("Fx")]
    [SerializeField] private ObjectPool alienExplosion;
    
    [Header("Projectiles & Missiles")]
    [SerializeField] private ObjectPool projectile;
    [SerializeField] private ObjectPool missile;

    // Entities
    public ObjectPool Alien => alien;
    
    // Projectiles & Missiles
    public ObjectPool Projectile => projectile;

    // Fx
    public ObjectPool AlienExplosion => alienExplosion;
}