using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the credits
/// </summary>
public class Credits : MonoBehaviour {

    // Update method
    public void Update()
    {
        // Checks if the key Escape was pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Will display a message on the console
            Debug.Log("Quit game");
            // Closes the program
            Application.Quit();
        }

    }
}
