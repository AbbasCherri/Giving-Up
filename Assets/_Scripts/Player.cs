using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public int health;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
    }
}
