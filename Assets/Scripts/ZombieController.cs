using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;

    void Start()
    {
        //Find the player GameObject using tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Check that NavMeshAgent is attached to the zombie GameObject
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        if (player != null)
        {
            //Set destination of the NavMeshAgent to the players position
            agent.SetDestination(player.position);
        }
    }
}