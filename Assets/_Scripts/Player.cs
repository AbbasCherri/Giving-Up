using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public int health;

    [SerializeField] private AudioClip[] clips;

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
        Audio.Instance.PlaySound(clips[0]);
        this.health -= damage;
    }
}
