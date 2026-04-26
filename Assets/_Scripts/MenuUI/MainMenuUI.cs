using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("First_Level"); 
    }

   
    public void ExitGame()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
}