using UnityEngine;

// TODO : Ajoutez toutes les références à vos ObjectPools ici.
//        Basez-vous sur le code existant.
public class ObjectPools : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private ObjectPool alien;
    [SerializeField] private ObjectPool portal;

    [Header("Fx")]
    [SerializeField] private ObjectPool alienExplosion;
    
    [Header("Projectiles & Missiles")]
    [SerializeField] private ObjectPool projectile;
    [SerializeField] private ObjectPool missile;

    // Entities
    public ObjectPool Alien => alien;
	public ObjectPool Portal => portal;

    // Fx
    public ObjectPool AlienExplosion => alienExplosion;
    
    // Projectiles & Missiles
    public ObjectPool Projectile => projectile;
    public ObjectPool Missile => missile;
}