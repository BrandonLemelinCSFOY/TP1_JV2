using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] private float alienSpawnTime = 0.5f;
    
    private float remainingAlienSpawnTime;

    private void OnEnable()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        remainingAlienSpawnTime = alienSpawnTime;
        yield return new WaitForSeconds(alienSpawnTime);
    }
}
