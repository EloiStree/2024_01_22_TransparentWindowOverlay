using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class KeepWindowFrameAtMousePositionMono : MonoBehaviour
{
    const int GWL_EXSTYLE = -20;
    const int WS_EX_LAYERED = 0x80000;
    const int WS_EX_TRANSPARENT = 0x20;
    const int LWA_COLORKEY = 0x1;

    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", SetLastError = true)]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    IntPtr hWnd;
    public bool m_removeFullScreenAtStart = true;
    private void Start()
    {
        if (m_removeFullScreenAtStart)
            SetFullscreen(false);
#if !UNITY_EDITOR
         hWnd = GetActiveWindow();
#endif
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
    public bool m_useOffset = false;
    public int m_offsetXPixel = 1;
    public int m_offsetYPixel = 10;
    int minWidth = 513;
    int minHeight = 513;

    private void Update()
    {
#if !UNITY_EDITOR
        

        // Set minimum size
        int minWidth = 512;
        int minHeight = 512;

        // Get cursor position
        POINT cursorPos;
        GetCursorPos(out cursorPos);

        int cursorX = cursorPos.X;
        int cursorY = cursorPos.Y;

        // Calculate the position to center the frame on the mouse cursor
        int frameWidth = minWidth; // Replace with your actual frame width
        int frameHeight = minHeight; // Replace with your actual frame height

        int centerX = (cursorX - frameWidth / 2)+ (m_useOffset? m_offsetXPixel:0);
        int centerY = (cursorY - frameHeight / 2) -(m_useOffset ? m_offsetYPixel:0);

        // Set the window position to topmost and at the calculated position
        SetWindowPos(hWnd, new IntPtr(-1), centerX, centerY, minWidth, minHeight, 0);


#endif
    }
}