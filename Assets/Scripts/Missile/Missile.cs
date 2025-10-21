using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float forwardSpeed = 200f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float explosionRadius = 20f;
    
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = transform.forward * forwardSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 explosionPosition = transform.position;
        
        // Jouer le son d'explosion
        if (audioClip != null)
        {
            Finder.GlobalAudioSource.PlayOneShot(audioClip);
        }
        
        var explosionEffect = Finder.ObjectPools.MissileExplosion.Get();
        explosionEffect.transform.position = explosionPosition;
        
        GameObject aoeObject = new GameObject("MissileAOE");
        MissileAOE aoeComponent = aoeObject.AddComponent<MissileAOE>();
        aoeComponent.Explode(explosionPosition);
        
        Finder.ObjectPools.Projectile.Release(this);
    }
}