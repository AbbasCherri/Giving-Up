using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class Manny : MonoBehaviour
{
    
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public AudioClip explosionSound;
    public Transform player;
    public Animator animator;
    public GameObject explosion;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        animator.SetFloat("speed",agent.velocity.magnitude);
        if (distance < 10f)
        {
            agent.SetDestination(player.position);
            if (distance>5f)
            {
                agent.speed = 3.5f;
            }
            else if (distance < 5f)
            {
                agent.speed = 6f;
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
        Instantiate(explosion, transform.position + offset, transform.rotation);
        Audio.Instance.PlaySound(explosionSound);
        Destroy(gameObject);
    }
}
