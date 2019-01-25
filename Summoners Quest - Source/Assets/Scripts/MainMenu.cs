using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Method to play the game
    /// </summary>
    public void PlayGame()
    {
        //Changes the scene to the next one
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Method to quit the game
    /// </summary>
    public void QuitGame()
    {
        // Shows a message on the console
        Debug.Log("Quit game");
        // Closes the program
        Application.Quit();
    }
}