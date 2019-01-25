using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSounds : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource forest = other.transform.GetChild(1).GetComponent<AudioSource>();
            AudioSource village = other.transform.GetChild(2).GetComponent<AudioSource>();
            if (forest.enabled)
            {
                forest.Stop();
                forest.enabled = false;
                village.enabled = true;
                village.Play();
            } else if (village.enabled)
            {
                village.Stop();
                village.enabled = false;
                forest.enabled = true;
                forest.Play();
            }
            
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
