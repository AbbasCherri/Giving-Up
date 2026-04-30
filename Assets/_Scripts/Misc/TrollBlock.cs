using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrollBlock : MonoBehaviour
{
    private bool hasFallen = false;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFallen)
        {
            hasFallen = true;
            rb.isKinematic = false;
            AudioManager.Instance.PlaySound("Laugh");
            Destroy(gameObject,0.5f);
        }
    }
}
