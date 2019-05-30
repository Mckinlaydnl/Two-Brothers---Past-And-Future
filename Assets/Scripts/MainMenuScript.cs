using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    // Audio Variables
    public AudioClip backgroundMusicSoundClip;

    public AudioSource backgroundMusicSoundSource;

    // Use this for initialization
    void Start ()
    {
        backgroundMusicSoundSource.clip = backgroundMusicSoundClip;
        backgroundMusicSoundSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
        //When the Enter key is pressed, play the game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Level 1");
        }

        // When the escape key is pressed, exit the application
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
