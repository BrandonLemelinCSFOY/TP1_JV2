using UnityEngine;
using System.Collections;

public class GameStateController : MonoBehaviour
{
    [Header("Game State")]
    private bool gameEnded = false;
    private int totalPortals;
    private int destroyedPortals = 0;

    private void Start()
    {
        // Compter le nombre total de portails au début
        CountTotalPortals();
        
        // S'abonner aux événements
        Finder.EventChannels.OnPortalDestroyed.AddListener(OnPortalDestroyed);
        Finder.EventChannels.OnPlayerDeath.AddListener(OnPlayerDeath);
    }

    private void OnDestroy()
    {
        // Se désabonner des événements
        if (Finder.EventChannels != null)
        {
            Finder.EventChannels.OnPortalDestroyed.RemoveListener(OnPortalDestroyed);
            Finder.EventChannels.OnPlayerDeath.RemoveListener(OnPlayerDeath);
        }
    }

    private void CountTotalPortals()
    {
        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
        totalPortals = portals.Length;
        Debug.Log($"Nombre total de portails: {totalPortals}");
    }

    private void OnPortalDestroyed(Portal portal)
    {
        if (gameEnded) return;

        destroyedPortals++;
        Debug.Log($"Portail détruit! ({destroyedPortals}/{totalPortals})");

        // Vérifier si tous les portails sont détruits
        if (destroyedPortals >= totalPortals)
        {
            StartCoroutine(TriggerVictory());
        }
    }

    private void OnPlayerDeath(SpaceMarine player)
    {
        if (gameEnded) return;

        Debug.Log("Le joueur est mort!");
        StartCoroutine(TriggerDefeat());
    }

    private IEnumerator TriggerVictory()
    {
        gameEnded = true;
        
        // Tuer tous les aliens restants
        KillAllRemainingAliens();
        
        // Attendre un petit délai pour l'effet dramatique
        yield return new WaitForSeconds(0.5f);
        
        // Déclencher l'événement de victoire
        Finder.EventChannels.OnGameVictory.Invoke();
        
        Debug.Log("VICTOIRE!");
    }

    private IEnumerator TriggerDefeat()
    {
        gameEnded = true;
        
        // Attendre un petit délai pour l'effet dramatique
        yield return new WaitForSeconds(0.5f);
        
        // Déclencher l'événement de défaite
        Finder.EventChannels.OnGameDefeat.Invoke();
        
        Debug.Log("DÉFAITE!");
    }

    private void KillAllRemainingAliens()
    {
        // Trouver tous les aliens actifs et les faire mourir
        GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
        foreach (GameObject alienObj in aliens)
        {
            Alien alien = alienObj.GetComponent<Alien>();
            if (alien != null)
            {
                // Forcer la mort de l'alien
                alien.Hurt(999);
            }
        }
    }
}
