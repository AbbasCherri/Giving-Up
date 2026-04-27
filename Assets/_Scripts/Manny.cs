using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class Manny_Move : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    public Transform player;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetFloat("speed",agent.velocity.magnitude);
        if (Vector3.Distance(transform.position, player.position) < 10f)
        {
            agent.SetDestination(player.position);
            if ( Vector3.Distance(transform.position,player.position)>5f)
            {
                agent.speed = 3.5f;
            }
            else if (Vector3.Distance(transform.position, player.position) < 5f)
            {
                agent.speed = 6f;
            }
        }

    }
}
