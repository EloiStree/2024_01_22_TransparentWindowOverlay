using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindowMono : MonoBehaviour
{

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);


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
    const int LWA_ALPHA = 0x2;

    private IntPtr hWnd;

    private const int GWL_STYLE = -16;

    public byte transparency = 10;

    private void Start()
    {
#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        // Set the window style to layered and transparent
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

        // Set the window position to topmost
        SetWindowPos(hWnd, new IntPtr(-1), 0, 0, 0, 0, 0);

        // Set the transparency (adjust the alpha value as needed)
        SetLayeredWindowAttributes(hWnd, 0, transparency, LWA_COLORKEY);
#endif
    }



    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

   
    // Define the RECT structure for GetWindowRect function
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }




    public string m_sourceVideo = "https://www.youtube.com/watch?v=RqgsGaMPZTw";

    [TextArea(0, 10)]
    public string m_devNote = "Don't forget too: "
        + "\n- Disable DXGI flip D3D11 in the Resolution and presentation Player menu" +
        "\n  - https://youtu.be/RqgsGaMPZTw?t=428"
        + "\n- Set the camera color to 0 0 0 0 in the background" +
        "\n  - https://youtu.be/RqgsGaMPZTw?t=454";

  

    [ContextMenu("Open the video online of Code Monkey")]
    public void OpenVideoHelpSource() { Application.OpenURL(m_sourceVideo); }

}
