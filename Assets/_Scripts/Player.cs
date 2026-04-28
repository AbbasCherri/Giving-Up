using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public int health;

    [SerializeField] private AudioClip clip;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        Audio.Instance.PlaySound(clip);
        this.health -= damage;
    }
}
