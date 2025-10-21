using System;
using UnityEngine;
using UnityEngine.AI;

public class AlienAI : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.destination = GameObject.FindWithTag("Player").transform.position;
    }
}
