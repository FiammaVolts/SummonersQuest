using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the pause menu
/// </summary>
public class PauseMenu : MonoBehaviour
{
    // Boolean variable to see if the game is pause, starting at false
    public static bool GameIsPaused = false;
    
    // GameObject variable for the UI
    public GameObject pauseMenuUI;
    // Instance of player
    public Player player;
    // Instance of CanvasManager
    private CanvasManager _canvasManager;

    /// <summary>
    /// Start method
    /// </summary>
    private void Start()
    {
        // Sets the canvasManager to the CanvasManager instance
        _canvasManager = CanvasManager.instance;
    }

    /// <summary>
    /// Update method
    /// </summary>
    void Update()
    {
        // An if to check if Escape was pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If GameisPaused is true
            if (GameIsPaused)
            {
                // Calls the Resume() method
                Resume();
            }
            // if it's false
            else
            {
                // It'll call the Pause() method
                Pause();
            }
        }
    }

    /// <summary>
    /// Method to resume the game
    /// </summary>
    public void Resume()
    {
        // Sets the pauseMenuUI to false
        pauseMenuUI.SetActive(false);
        // TimeScale set to 1
        Time.timeScale = 1f;
        // Sets GameIsPaused to false
        GameIsPaused = false;
        // Calls ShowInventory
        _canvasManager.ShowInventory();
        // Resumes the camera
        player.GetComponentInChildren<Camera>().enabled = true;
    }

    /// <summary>
    /// Method to pause the game
    /// </summary>
    void Pause()
    {
        // Sets the pauseMenuUI to true
        pauseMenuUI.SetActive(true);
        // Sets the timeScale to 0
        Time.timeScale = 0f;
        // SetsGameIsPaused to true
        GameIsPaused = true;
        // Sets the cursor's visibility to true
        Cursor.visible = true;
        // Calls HideInventory()
        _canvasManager.HideInventory();
        // Stops the camera
        player.GetComponentInChildren<Camera>().enabled = false;
    }

    /// <summary>
    /// Method to load the menu
    /// </summary>
    public void LoadMenu()
    {
        // Sets the timeScale to 1
        Time.timeScale = 1f;    
        // Loads the scene "Menu"
        SceneManager.LoadScene("Menu");
        // Sets the cursor's visibility to true
        Cursor.visible = true;
    }

    /// <summary>
    /// Method to quit the game
    /// </summary>
    public void QuitGame()
    {
        // Shows the message on the console
        Debug.Log("Quit game");
        // Closes the program
        Application.Quit();
    }
}
