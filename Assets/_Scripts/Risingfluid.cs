using UnityEngine;
using UnityEngine.SceneManagement;

public class Risingfluid : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 1f;
    [SerializeField] private float maxHeight = 10f;

    private float currentHeight = 0f;

    void Update()
    {
        if (currentHeight < maxHeight)
        {
            float delta = riseSpeed * Time.deltaTime;
            currentHeight += delta;

            transform.localScale += new Vector3(0, delta, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}