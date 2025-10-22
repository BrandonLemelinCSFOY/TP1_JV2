using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private int healAmount = 25;
    [SerializeField] private float autoDestroyTime = 10f;

    private void OnEnable()
    {
        // Démarrer le timer de destruction automatique
        StartCoroutine(AutoDestroyTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        var spaceMarine = other.GetComponent<SpaceMarine>();
        if (spaceMarine != null)
        {
            // Restaurer la santé du joueur
            spaceMarine.RestoreHealth(healAmount);

            // Jouer le son de collecte
            if (audioClip != null)
            {
                Finder.GlobalAudioSource.PlayOneShot(audioClip);
            }

            // Retourner l'objet au pool immédiatement
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