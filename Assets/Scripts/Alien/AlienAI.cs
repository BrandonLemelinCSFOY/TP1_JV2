using System;
using UnityEngine;
using UnityEngine.AI;

public class AlienAI : MonoBehaviour
{
    [SerializeField] GameObject destination;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.destination = destination.transform.position;
    }
}
