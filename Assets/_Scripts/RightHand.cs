using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightHand : MonoBehaviour
{
    Animator animator;
    public bool isHolding()
    {
        if (Input.GetMouseButton(1))
        {
            return true;
        }

        return false;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding())
        {
            animator.SetBool("isHolding", true);
        }
        else
        {
            animator.SetBool("isHolding", false);
        }
    }
}
