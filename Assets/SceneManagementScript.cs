using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Difficulty Selection Scene");
    }

    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions Scene");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
