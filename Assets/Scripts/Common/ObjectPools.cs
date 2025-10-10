using System;
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
    [SerializeField] private ObjectPool missileExplosion;
    
    [Header("Projectiles & Missiles")]
    [SerializeField] private ObjectPool projectile;
    [SerializeField] private ObjectPool missile;

    private static ObjectPools instance;
    private static ObjectPools Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindWithTag("ObjectPools").GetComponent<ObjectPools>();
            return instance;
        }
    }
    
    // Entities
    public ObjectPool Alien => alien;
	public ObjectPool Portal => portal;

    // Fx
    public static ObjectPool AlienExplosion => Instance.alienExplosion;
    public ObjectPool MissileExplosion => missileExplosion;
    
    // Projectiles & Missiles
    public ObjectPool Projectile => projectile;
    public ObjectPool Missile => missile;
}