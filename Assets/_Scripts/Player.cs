using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public int health;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Time.timeScale = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
    }
}
