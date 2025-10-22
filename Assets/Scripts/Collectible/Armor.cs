using UnityEngine;
using System.Collections;

public class Armor : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float invulnerabilityDuration = 5f;
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
            // TODO: Rendre le joueur invulnérable pendant 5 secondes
            // spaceMarine.MakeInvulnerable(invulnerabilityDuration);
            
            if (audioClip != null)
            {
                Finder.GlobalAudioSource.PlayOneShot(audioClip);
            }
            
            Finder.ObjectPools.ArmorCollectible.Release(this);
        }
    }

    private IEnumerator AutoDestroyTimer()
    {
        // Attendre 10 secondes
        yield return new WaitForSeconds(autoDestroyTime);
        
        // Si l'objet n'a pas été ramassé, le retourner au pool
        Finder.ObjectPools.ArmorCollectible.Release(this);
    }
}
