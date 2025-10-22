using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private int healAmount = 25;
    [SerializeField] private float autoDestroyTime = 10f;

    private void OnEnable()
    {
        StartCoroutine(AutoDestroyTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        var spaceMarine = other.GetComponent<SpaceMarine>();
        if (spaceMarine != null)
        {
            spaceMarine.RestoreHealth(healAmount);
            
            if (audioClip != null)
            {
                Finder.GlobalAudioSource.PlayOneShot(audioClip);
            }
            
            Finder.ObjectPools.HealthCollectible.Release(this);
        }
    }

    private IEnumerator AutoDestroyTimer()
    {
        // Attendre 10 secondes
        yield return new WaitForSeconds(autoDestroyTime);
        
        // Si l'objet n'a pas été ramassé, le retourner au pool
        Finder.ObjectPools.HealthCollectible.Release(this);
    }
}