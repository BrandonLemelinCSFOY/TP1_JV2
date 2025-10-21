using UnityEngine;

// TODO : Ajoutez toutes les références à vos ObjectPools ici.
//        Basez-vous sur le code existant.
public class ObjectPools : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private ObjectPool alien;
    [SerializeField] private ObjectPool portal;
    
    [Header("Collectibles")]
    [SerializeField] private ObjectPool healthCollectible;
    [SerializeField] private ObjectPool missileCollectible;
    [SerializeField] private ObjectPool armorCollectible;

    [Header("Fx")]
    [SerializeField] private ObjectPool alienExplosion;
    [SerializeField] private ObjectPool missileExplosion;
    [SerializeField] private ObjectPool portalExplosion;

    [Header("Projectiles & Missiles")]
    [SerializeField] private ObjectPool projectile;
    [SerializeField] private ObjectPool missile;
    
    // Entities
    public ObjectPool Alien => alien;
	public ObjectPool Portal => portal;
    
    // Collectibles
    public ObjectPool HealthCollectible => healthCollectible;
    public ObjectPool MissileCollectible => missileCollectible;
    public ObjectPool ArmorCollectible => armorCollectible;

    // Fx
    public ObjectPool AlienExplosion => alienExplosion;
    public ObjectPool MissileExplosion => missileExplosion;
    public ObjectPool PortalExplosion => portalExplosion;
    
    // Projectiles & Missiles
    public ObjectPool Projectile => projectile;
    public ObjectPool Missile => missile;
}