using UnityEngine;

/// <summary>
/// Class for the sound
/// </summary>
public class SwitchSounds : MonoBehaviour {

    /// <summary>
    /// Method that will play the sounds
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // An if that checks the player
        if (other.tag == "Player")
        {
            // Creates an audio source for the forest's sound
            AudioSource forest = other.transform.GetChild(1).GetComponent<AudioSource>();
            // Creates an audio source for the village's sound
            AudioSource village = other.transform.GetChild(2).GetComponent<AudioSource>();

            // An if to see if the forest audio is playing
            if (forest.enabled)
            {
                // Stops the audio
                forest.Stop();
                // Sets it at false
                forest.enabled = false;
                // Sets the village audio at true
                village.enabled = true;
                // Begins playing the village audio
                village.Play();
              // An if to see if it's the village audio playing
            } else if (village.enabled)
            {
                // Stops the audio
                village.Stop();
                // Sets it at false
                village.enabled = false;
                // Sets the forest audio at true
                forest.enabled = true;
                // Begins playing the forest audio
                forest.Play();
            }            
        }
    }
}
