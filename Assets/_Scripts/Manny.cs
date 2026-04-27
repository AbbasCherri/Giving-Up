using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Manny_Move : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    public Transform player;
    void Update()
    {
        if (player)
        {
            agent.SetDestination(player.position);
        }
    }
}
