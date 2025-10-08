using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 200f;
    [SerializeField] private int damage = 10;
    
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
        var hurtable = other.gameObject.GetComponent<IHurtable>();
        if (hurtable != null) hurtable.Hurt();
        Finder.ObjectPools.Projectile.Release(this);
    }
}