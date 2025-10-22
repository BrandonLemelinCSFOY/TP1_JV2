using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float forwardSpeed = 200f;
    [SerializeField] private int damage = 1;
    
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (audioClip != null)
        {
            Finder.GlobalAudioSource.PlayOneShot(audioClip);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = transform.forward * forwardSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var hurtable = other.gameObject.GetComponent<IHurtable>();
        if (hurtable != null) hurtable.Hurt(damage);
        Finder.ObjectPools.Projectile.Release(this);
    }
}