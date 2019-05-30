using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundsScript : MonoBehaviour {

    // Use this for initialization

    // Public variables
    public PlayerOneHealthBar playerOneHealthBarScript;
    public PlayerTwoHealthBar playerTwoHealthBarScript;

    // Private Variables
    private float playerOneHealthPoints;
    private float playerTwoHealthPoints;
    private static int playerOneWins;
    private static int playerTwoWins;
    private bool playersHaveDrawed = false;

	void Start () {
        playerOneHealthBarScript.resetHealth();
    }
	
	// Update is called once per frame
	void Update () {
        // Set the health points to the players health points. Check this every frame.
        playerOneHealthPoints = playerOneHealthBarScript.amountOfHealthPoints();
        playerTwoHealthPoints = playerTwoHealthBarScript.amountOfHealthPoints();

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log(playerOneWins);
        }

    }

    public void nextRound()
    {
        //  Check what scene should be loaded next by seeing who has the most hit points, adding a win to whoever won and loading the appropriate scene.
        if (playerOneHealthPoints>playerTwoHealthPoints)
        {
            playerOneWins = playerOneWins + 1;

            if (playerOneWins == playerTwoWins)
            {
                playersHaveDrawed = true;
                SceneManager.LoadScene("Draw Screen");
            }

            if (playerOneWins < 2 && playersHaveDrawed == false)
            {
                {
                    SceneManager.LoadScene("Player One win screen");
                }
            }
            if (playerOneWins >= 2)
            {
                SceneManager.LoadScene("PlayerOneOverallWin");
            }
        }
        if (playerTwoHealthPoints>playerOneHealthPoints)
        {
            playerTwoWins = playerTwoWins + 1;
            if (playerOneWins == playerTwoWins)
            {
                playersHaveDrawed = true;
                SceneManager.LoadScene("Draw Screen");
            }

            
            if (playerTwoWins < 2 && playersHaveDrawed == false)
            {
                SceneManager.LoadScene("Player Two win screen");
            }
            if (playerTwoWins >= 2)
            {
                SceneManager.LoadScene("PlayerTwoOverallWin");
            }
        }
        
    }

   
}
