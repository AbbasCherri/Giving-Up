using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    Animator animator;
    public enum WhichHand
    {
        Left =0,
        Right = 1
    }
    [SerializeField] public WhichHand whichHand;
    public bool isHolding()
    {
        if (Input.GetMouseButton((int) whichHand))
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
