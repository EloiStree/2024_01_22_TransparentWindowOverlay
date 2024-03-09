using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindowMono : MonoBehaviour
{
    public string m_sourceVideo = "https://www.youtube.com/watch?v=RqgsGaMPZTw";

    [TextArea(0, 10)]
    public string m_devNote = "Don't forget too: " 
        +"\n- Disable DXGI flip D3D11 in the Resolution andd presentation Player menu"
        +"\n- Set the camera color to 0 0 0 0 in the background";

    [ContextMenu("Open the video online of Code Monkey")]
    public void OpenVideoHelpSource() { Application.OpenURL(m_sourceVideo); }


    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern int SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    private const int GWL_EXSTYLE = -20;
    private const uint WS_EX_LAYERED = 0x00080000;
    private const uint WS_EX_TRANSPARENT = 0x00000020;
    private const uint LWA_COLORKEY = 0x00000001;

    private IntPtr hWnd;

    private void Start()
    {
#if !UNITY_EDITOR_
        hWnd = GetActiveWindow();

        // Set the window style to layered and transparent
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

        // Set the window position to topmost
        SetWindowPos(hWnd, new IntPtr(-1), 0, 0, 0, 0, 0);

        // Set the transparency (adjust the alpha value as needed)
        SetLayeredWindowAttributes(hWnd, 0, 128, LWA_COLORKEY);
#endif

    }


}
