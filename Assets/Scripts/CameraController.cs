using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

   
    private Transform[] playerTransforms; // Array for all the players
    public float yOffSet = 2.0f; //Private variable to store the yoffset distance between the players and camera
    public float minDistance = 100f;
    private float xMin, xMax, yMin, yMax;

    // Use this for initialization
    void Start()
    {
        // Find all the players and fill the array with them
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        playerTransforms = new Transform[allPlayers.Length];
        for(int i = 0; i < allPlayers.Length; i++)
        {
            playerTransforms[i] = allPlayers[i].transform;
        }
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // If the escape key is pressed, exit the application
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        // For debugging
        if (playerTransforms.Length == 0)
        {
            Debug.Log("Idiot, there are no players");
            return;
        }

       // Moves camera depending on the players position to each other.
        xMin = xMax = playerTransforms[0].position.x;
        yMin = yMax = playerTransforms[0].position.y;
        for(int i = 1; i < playerTransforms.Length; i++)
        {
            if (playerTransforms[i].position.x < xMin)
                xMin = playerTransforms[i].position.x;

            if (playerTransforms[i].position.x > xMax)
                xMax = playerTransforms[i].position.x;

            if (playerTransforms[i].position.y < yMin)
                yMin = playerTransforms[i].position.y;

            if (playerTransforms[i].position.y > yMax)
                yMax = playerTransforms[i].position.y;
        }

        float xMiddle = (xMin + xMax) / 2;
        float yMiddle = (yMin + yMax) / 2;
        float distance = xMax - xMin;
        if (distance < minDistance)
            distance = minDistance;


        
        transform.position = new Vector3(xMiddle, (float)(yMiddle + 1.5), (float)(-distance-4));
       

    }
}
