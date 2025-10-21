using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private float alienSpawnTime = 0.5f;
    
    private float remainingAlienSpawnTime = 0.5f;

    private void Update()
    {
        UpdateSpawning();
    }

    private void OnEnable()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        var delay = new WaitForSeconds(alienSpawnTime);
        while (remainingAlienSpawnTime > 0)
        {
            remainingAlienSpawnTime -= alienSpawnTime;
            yield return delay;
        }
    }

    private void UpdateSpawning()
    {
        if (remainingAlienSpawnTime <= 0)
        {
            if (Finder.ObjectPools.Alien.ActiveCount <= 20)
            {
                GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
                if (portals.Length > 0)
                {
                    GameObject randomPortal = portals[Random.Range(0, portals.Length)];
                    Finder.ObjectPools.Alien.Place(randomPortal.transform.position);
                    remainingAlienSpawnTime = alienSpawnTime;
                }
                else return;
            }
            
        }
    }
}
