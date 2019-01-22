using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public Player player;
    private CanvasManager _canvasManager;


    private void Start()
    {
        _canvasManager = CanvasManager.instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        _canvasManager.ShowInventory();
        player.GetComponentInChildren<Camera>().enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        _canvasManager.HideInventory();
        player.GetComponentInChildren<Camera>().enabled = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;        
        SceneManager.LoadScene("Menu");
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
