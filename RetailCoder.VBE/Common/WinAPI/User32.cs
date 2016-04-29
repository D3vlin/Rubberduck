using System;
using System.Runtime.InteropServices;

namespace Rubberduck.Common.WinAPI
{
    public enum KeyModifier : uint
    {
        ALT = 0x1,
        CONTROL = 0x2,
        SHIFT = 0x4,
        WIN = 0x8
    }

    /// <summary>
    /// Exposes User32.dll API.
    /// </summary>
    public static class User32
    {
        /// <summary>
        /// Defines a system-wide hot key.
        /// </summary>
        /// <param name="hWnd">A handle to the window that will receive WM_HOTKEY messages generated by the hot key. 
        /// If this parameter is NULL, WM_HOTKEY messages are posted to the message queue of the calling thread and must be processed in the message loop.</param>
        /// <param name="id">The identifier of the hot key. 
        /// If the hWnd parameter is NULL, then the hot key is associated with the current thread rather than with a particular window. 
        /// If a hot key already exists with the same hWnd and id parameters</param>
        /// <param name="fsModifiers">The keys that must be pressed in combination with the key specified by the uVirtKey parameter in order to generate the WM_HOTKEY message. 
        /// The fsModifiers parameter can be a combination of the following values.</param>
        /// <param name="vk">The virtual-key code of the hot key</param>
        /// <returns>If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, IntPtr id, uint fsModifiers, uint vk);


        /// <summary>
        /// Frees a hot key previously registered by the calling thread.
        /// </summary>
        /// <param name="hWnd">A handle to the window associated with the hot key to be freed. This parameter should be NULL if the hot key is not associated with a window.</param>
        /// <param name="id">The identifier of the hot key to be freed.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, IntPtr id);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, WndProc dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr CallWindowProc(WndProc lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public delegate IntPtr WndProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working). 
        /// The system assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
        /// </summary>
        /// <returns>The return value is a handle to the foreground window. 
        /// The foreground window can be NULL in certain circumstances, such as when a window is losing activation.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Retrieves the name of the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="lpClassName">The class name string (maximum 256 characters).</param>
        /// <param name="nMaxCount">The length of the lpClassName buffer, in characters. 
        /// The buffer must be large enough to include the terminating null character; otherwise, the class name string is truncated to nMaxCount-1 characters.</param>
        /// <returns>If the function succeeds, the return value is the number of characters copied to the buffer, not including the terminating null character. 
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, string lpClassName, int nMaxCount);

        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window and, optionally, 
        /// the identifier of the process that created the window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpdwProcessId">A pointer to a variable that receives the process identifier. 
        /// If this parameter is not NULL, GetWindowThreadProcessId copies the identifier of the process to the variable; otherwise, it does not.</param>
        /// <returns>The return value is the identifier of the thread that created the window.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="processId">IntPtr.Zero</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        /// <summary>
        /// Creates a timer with the specified time-out value.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be associated with the timer. 
        /// This window must be owned by the calling thread. 
        /// If a NULL value for hWnd is passed in along with an nIDEvent of an existing timer, 
        /// that timer will be replaced in the same way that an existing non-NULL hWnd timer will be.</param>
        /// <param name="nIDEvent">A nonzero timer identifier. 
        /// If the hWnd parameter is NULL, and the nIDEvent does not match an existing timer then it is ignored and a new timer ID is generated. 
        /// If the hWnd parameter is not NULL and the window specified by hWnd already has a timer with the value nIDEvent, 
        /// then the existing timer is replaced by the new timer. When SetTimer replaces a timer, the timer is reset. 
        /// Therefore, a message will be sent after the current time-out value elapses, but the previously set time-out value is ignored. 
        /// If the call is not intended to replace an existing timer, nIDEvent should be 0 if the hWnd is NULL.</param>
        /// <param name="uElapse">The time-out value, in milliseconds.</param>
        /// <param name="lpTimerFunc">A pointer to the function to be notified when the time-out value elapses. 
        /// For more information about the function, see TimerProc. If lpTimerFunc is NULL, the system posts a WM_TIMER message to the application queue. 
        /// The hwnd member of the message's MSG structure contains the value of the hWnd parameter.</param>
        /// <returns>If the function succeeds and the hWnd parameter is NULL, the return value is an integer identifying the new timer. 
        /// An application can pass this value to the KillTimer function to destroy the timer. 
        /// If the function succeeds and the hWnd parameter is not NULL, then the return value is a nonzero integer. 
        /// An application can pass the value of the nIDEvent parameter to the KillTimer function to destroy the timer.
        /// If the function fails to create a timer, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);
        public delegate void TimerProc(IntPtr hWnd, WindowLongFlags uMsg, IntPtr nIDEvent, uint dwTime);

        /// <summary>
        /// Creates a timer with the specified time-out value.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be associated with the timer. 
        /// This window must be owned by the calling thread. 
        /// If a NULL value for hWnd is passed in along with an nIDEvent of an existing timer, 
        /// that timer will be replaced in the same way that an existing non-NULL hWnd timer will be.</param>
        /// <param name="nIDEvent">A nonzero timer identifier. 
        /// If the hWnd parameter is NULL, and the nIDEvent does not match an existing timer then it is ignored and a new timer ID is generated. 
        /// If the hWnd parameter is not NULL and the window specified by hWnd already has a timer with the value nIDEvent, 
        /// then the existing timer is replaced by the new timer. When SetTimer replaces a timer, the timer is reset. 
        /// Therefore, a message will be sent after the current time-out value elapses, but the previously set time-out value is ignored. 
        /// If the call is not intended to replace an existing timer, nIDEvent should be 0 if the hWnd is NULL.</param>
        /// <param name="uElapse">The time-out value, in milliseconds.</param>
        /// <param name="lpTimerFunc">A pointer to the function to be notified when the time-out value elapses. 
        /// For more information about the function, see TimerProc. If lpTimerFunc is NULL, the system posts a WM_TIMER message to the application queue. 
        /// The hwnd member of the message's MSG structure contains the value of the hWnd parameter.</param>
        /// <returns>If the function succeeds and the hWnd parameter is NULL, the return value is an integer identifying the new timer. 
        /// An application can pass this value to the KillTimer function to destroy the timer. 
        /// If the function succeeds and the hWnd parameter is not NULL, then the return value is a nonzero integer. 
        /// An application can pass the value of the nIDEvent parameter to the KillTimer function to destroy the timer.
        /// If the function fails to create a timer, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, IntPtr lpTimerFunc);

        /// <summary>
        /// Destroys the specified timer.
        /// </summary>
        /// <param name="hWnd">A handle to the window associated with the specified timer. 
        /// This value must be the same as the hWnd value passed to the SetTimer function that created the timer.</param>
        /// <param name="uIDEvent">The timer to be destroyed. 
        /// If the window handle passed to SetTimer is valid, this parameter must be the same as the nIDEvent value passed to SetTimer. 
        /// If the application calls SetTimer with hWnd set to NULL, this parameter must be the timer identifier returned by SetTimer.</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool KillTimer(IntPtr hWnd, IntPtr uIDEvent);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int GetRawInputData(IntPtr hRawInput, DataCommand command, [Out] out InputData buffer, [In, Out] ref int size, int cbSizeHeader);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int GetRawInputData(IntPtr hRawInput, DataCommand command, [Out] IntPtr pData, [In, Out] ref int size, int sizeHeader);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetRawInputDeviceInfo(IntPtr hDevice, RawInputDeviceInfo command, IntPtr pData, ref uint size);

        [DllImport("user32.dll")]
        private static extern uint GetRawInputDeviceInfo(IntPtr hDevice, uint command, ref DeviceInfo data, ref uint dataSize);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint numberDevices, uint size);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterRawInputDevices(RawInputDevice[] pRawInputDevice, uint numberDevices, uint size);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr notificationFilter, DeviceNotification flags);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterDeviceNotification(IntPtr handle);

        /// <summary>
        /// A helper function that returns <c>true</c> when the specified handle is that of the foreground window.
        /// </summary>
        /// <param name="mainWindowHandle">The handle for the VBE's MainWindow.</param>
        /// <returns></returns>
        public static bool IsVbeWindowActive(IntPtr mainWindowHandle)
        {
            uint vbeThread;
            GetWindowThreadProcessId(mainWindowHandle, out vbeThread);

            uint hThread;
            GetWindowThreadProcessId(GetForegroundWindow(), out hThread);

            return (IntPtr)hThread == (IntPtr)vbeThread;
        }
    }
}