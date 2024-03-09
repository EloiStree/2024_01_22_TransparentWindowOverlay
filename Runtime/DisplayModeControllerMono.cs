using UnityEngine;

public class DisplayModeController : MonoBehaviour
{

    public bool m_startInFullScreenMode = true;
    public int m_displayIndexDefault = 1;
    void Start()
    {
        SetFullscreen(m_startInFullScreenMode);
        // Set the game window to the second monitor
        SetGameWindowToSecondMonitor();
    }

    void SetGameWindowToSecondMonitor()
    {
        // Check if there are multiple displays
        if (Display.displays.Length > 1)
        {
            // Assuming the second display is index 1 (change it based on your setup)
            Display secondDisplay = Display.displays[1];

            // Set the game window to the second monitor's resolution and position it
            Screen.SetResolution(secondDisplay.systemWidth, secondDisplay.systemHeight, true);
        
        }
        else
        {
            Debug.LogWarning("There is only one display available.");
        }
    }

    public void ToggleFullscreen()
    {
        // Toggle between fullscreen and windowed mode
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void SetFullscreen(bool wantFullScreen)
    {
        // Toggle between fullscreen and windowed mode
        Screen.fullScreen = wantFullScreen;
    }
}