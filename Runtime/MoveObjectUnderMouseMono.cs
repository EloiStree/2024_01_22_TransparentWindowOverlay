
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveObjectUnderMouseMono : MonoBehaviour
{
    public InputActionReference m_mouseInputPosition;
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
        m_mouseInputPosition.action.Enable();
    }


    void Update()
    {
        Vector2 mousePositoin = m_mouseInputPosition.action.ReadValue<Vector2>();
        // Get the current mouse position in screen space
        Vector3 mousePositionScreen = new Vector3(mousePositoin.x, mousePositoin.y, distance);

        // Convert the mouse position from screen space to world space
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        // Set the object's position to the converted mouse position
        m_targetObject.position = mousePositionWorld;
    }
}