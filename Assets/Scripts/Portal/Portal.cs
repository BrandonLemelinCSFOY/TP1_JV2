using UnityEngine;

public class Portal : MonoBehaviour, IHurtable
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private int health = 10;
    [SerializeField] private float spawnRate = 0.5f;
    [SerializeField] private int maxAliens = 10;

    private void Die()
    {
        // Déclencher l'événement de destruction du portail
        Finder.EventChannels.OnPortalDestroyed.Invoke(this);
        
        Finder.ObjectPools.PortalExplosion.Place(transform.position);
        Finder.GlobalAudioSource.PlayOneShot(audioClip);
        
        var randomNumber = Random.Range(0, 3);
        var collectiblePosition = new Vector3(transform.position.x, 0, transform.position.z);
        switch (randomNumber)
        {
            case 0:
                Finder.ObjectPools.ArmorCollectible.Place(collectiblePosition);
                break;
            case 1:
                Finder.ObjectPools.MissileCollectible.Place(collectiblePosition);
                break;
            case 2:
                Finder.ObjectPools.HealthCollectible.Place(collectiblePosition);
                break;
        }
        
        Destroy(gameObject);
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