using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInterface : MonoBehaviour
{

    public Transform rotateAxis;

    public float axisMoveSpeed;

    public float distanceMin;
    public float distanceMax;
    public float distanceScrollRate;

    public Vector2 sensitivity;
    public float yMin;
    public float yMax;

    Vector2 cameraMove;
    float distance;

    // Use this for initialization
    void Start()
    {
        distance = distanceMax;

        transform.rotation = Quaternion.Euler(-90, 0, 0);
        transform.position = rotateAxis.transform.position + (-transform.forward * distance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void LateUpdate()
    {
        /*
        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;

            cameraMove.x += Input.GetAxis("Mouse X") * speed.x * Time.deltaTime;
            cameraMove.y += Input.GetAxis("Mouse Y") * speed.y * Time.deltaTime;
            cameraMove.y = Mathf.Clamp(cameraMove.y, yMin, yMax);
        }
        else
        {
            Cursor.visible = true;
        }

        transform.rotation = Quaternion.Euler(cameraMove.y, cameraMove.x, 0);
        transform.position = rotateAxis.transform.position + (-transform.forward * distance);

        // This can be adapted for a third-person character as well! Add a spherecast behind the camera to detect if there is terrain behind it, and move the camera forward to prevent it from colliding.
        */

        if (Input.GetMouseButton(1)) // If right mouse button is clicked, accept button inputs
        {
            Cursor.visible = false; // Hide cursor

            cameraMove.x += Input.GetAxis("Mouse X") * sensitivity.x * distance * Time.deltaTime; // Accept mouse inputs
            cameraMove.y += -Input.GetAxis("Mouse Y") * sensitivity.y * distance * Time.deltaTime;
            cameraMove.y = Mathf.Clamp(cameraMove.y, yMin, yMax); // Clamp Y rotation so you can't flip the camera upside down
        }
        else
        {
            Cursor.visible = true;
        }

        distance -= Input.GetAxis("Mouse ScrollWheel") * distanceScrollRate * distance; // Distance input value is based on mouse scrollwheel, times the scrolling speed, times distance to allow the player to quickly zoom in if they are far away
        distance = Mathf.Clamp(distance, distanceMin, distanceMax); // Distance is clamped between the minimum and maximum acceptable amounts

        Vector2 cameraMoveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // Obtains movement inputs
        cameraMoveInput.Normalize(); // Normalises direction inputs so movement is the same speed in every direction
        Vector3 axisMove = new Vector3(cameraMoveInput.x * axisMoveSpeed * distance, 0, cameraMoveInput.y * axisMoveSpeed * distance); // New vector3 multiplies input vectors by movement speed, by distance from centre axis, to allow quick movement if zoomed out
        axisMove = rotateAxis.transform.rotation * axisMove; // Movement vector3 is multiplied by direction so movement keys are the same regardless of camera direction

        rotateAxis.transform.position += axisMove * Time.deltaTime; // Moves camera axis
        transform.rotation = Quaternion.Euler(cameraMove.y, cameraMove.x, 0); // Rotates camera
        rotateAxis.transform.rotation = Quaternion.Euler(0, cameraMove.x, 0); // Rotates rotation axis so that movement controls always move in the same direction relative to camera direction
        transform.position = rotateAxis.transform.position + (-transform.forward * distance); // Camera is moved and zoomed in and out accordingly

        // This can be adapted for a third-person character as well! Add a spherecast behind the camera to detect if there is terrain behind it, and move the camera forward to prevent it from colliding.
    }

}
