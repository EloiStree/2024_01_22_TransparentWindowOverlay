
using UnityEngine;

public class MoveObjectUnderMouseMono : MonoBehaviour
{
    public float distance = 10f; // Distance to move the object
    public Camera mainCamera; // Reference to the main camera
    public Transform m_targetObject;
    void Start()
    {
        if (mainCamera == null)
        {
            // If mainCamera is not assigned, try to find it in the scene
            mainCamera = Camera.main;
        }

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Please assign the main camera to the script.");
        }
    }


    void Update()
    {
        // Get the current mouse position in screen space
        Vector3 mousePositionScreen = Input.mousePosition;

        // Set the distance of the object from the camera
        mousePositionScreen.z = distance;

        // Convert the mouse position from screen space to world space
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        // Set the object's position to the converted mouse position
        m_targetObject.position = mousePositionWorld;
    }
}