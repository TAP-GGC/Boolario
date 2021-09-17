using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private Transform playerTransform; //Transform class manipulates the position, scale, and rotation of Unity objects

    void Start() 
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //creates a variable to hold the player object named "Boolrio"
    }

    void LateUpdate()
    {
        Vector3 currentCameraPosition = transform.position; //variable to hold the camera's current position
        currentCameraPosition.x = playerTransform.position.x + 7; //sets the current x-position of the camera to the x-position of the player
        //currentCameraPosition.y = playerTransform.position.y + 1; //sets the current y-position of the camera to the y-position of the player
        transform.position = currentCameraPosition; //stores the temporary position of the camera into the actual position
    }
}