using System.Collections;
using UnityEngine;
public class Alien : MonoBehaviour, IHurtable
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private int health = 1;
    [SerializeField] private int damage = 10;
    [SerializeField] private int moveSpeed = 10;
    [SerializeField] private int rotationSpeed = 120;

    private void OnTriggerEnter(Collider other)
    {
        var hurtable = other.gameObject.GetComponent<IHurtable>();
        var player = other.gameObject.GetComponent<SpaceMarine>();
        var portal = other.gameObject.GetComponent<Portal>();
        if (portal != null)
        {
            return;
        }
        if (player != null)
        {
            player.Hurt(damage);
            Hurt(health);
        }
    }

    private void Die()
    {
        Finder.ObjectPools.AlienExplosion.Place(transform.position);
        Finder.GlobalAudioSource.PlayOneShot(audioClip);
        health = 1;
        Finder.ObjectPools.Alien.Release(this);
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void Hurt(int damage)
    {
        health -= damage;
    }
}