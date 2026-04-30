using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;


public class Manny : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform playerPos;
    private Player player;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = playerPos.GetComponent<Player>();
        agent.angularSpeed = data.rotationSpeed;
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerPos.position);
        animator.SetFloat("speed",agent.velocity.magnitude);
        if (distance <  data.detectionRange)
        {
            agent.SetDestination(playerPos.position);
            if (distance>data.chaseRange)
            {
                agent.speed = data.walkSpeed;
            }
            else
            {
                agent.speed = data.runSpeed;
                if (distance < 1f)
                {
                    Explode();
                }
            }

        }

    }

    void Explode()
    {
        Vector3 offset = new Vector3(0, 1.5f, 0);
        ObjectPool.Instance.GetFromPool("Explosion", transform.position + offset, transform.rotation);
        if (player)
        {
            player.TakeDamage(data.damage);
            AudioManager.Instance.PlaySound("Explosion");
        }
        Destroy(gameObject);
    }
}
