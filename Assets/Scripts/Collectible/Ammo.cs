using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
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
            if (Finder.GameController.GetCurrentMissileCount() < Finder.GameController.GetMaxMissileCount())
            {
                for (int i = 0; i < 5; i++)
                {
                    Finder.GameController.AddMissile();
                    // S'arrêter si on atteint le maximum de missiles
                    if (Finder.GameController.GetCurrentMissileCount() >= Finder.GameController.GetMaxMissileCount())
                        break;
                }
                
                if (audioClip != null)
                {
                    Finder.GlobalAudioSource.PlayOneShot(audioClip);
                }
                
                Finder.ObjectPools.MissileCollectible.Release(this);
            }
        }
    }

    private IEnumerator AutoDestroyTimer()
    {
        // Attendre 10 secondes
        yield return new WaitForSeconds(autoDestroyTime);
        
        // Si l'objet n'a pas été ramassé, le retourner au pool
        Finder.ObjectPools.MissileCollectible.Release(this);
    }
}