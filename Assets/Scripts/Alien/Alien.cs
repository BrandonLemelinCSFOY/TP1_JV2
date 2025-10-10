using System.Collections;
using UnityEngine;
public class Alien : MonoBehaviour, IHurtable
{
    [SerializeField] private int health = 1;
    [SerializeField] private int damage = 10;
    [SerializeField] private int moveSpeed = 10;
    [SerializeField] private int rotationSpeed = 120;

    private void OnTriggerEnter(Collider other)
    {
        var hurtable = other.gameObject.GetComponent<IHurtable>();
        if (hurtable != null)
        {
            hurtable.Hurt();
            Hurt();
        }
    }

    private void Die()
    {
        Finder.ObjectPools.AlienExplosion.Place(transform.position);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void Hurt()
    {
        health--;
    }
}