using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;


namespace HZJ.DxWinForm.Utility.CommCls
{
    /// <summary>
    /// 单实例应用程序
    /// </summary>
    public class SingleInstanceApplication
    {
        /// <summary>
        /// 判断应用是否运行
        /// </summary>
        /// <param name="processName">程序名称</param>
        /// <param name="exit"> 是否退出</param>
        /// <returns></returns>
        public static IDisposable Guard(out bool exit)
        {
            Process currentProcess = Process.GetCurrentProcess();
            var processName = currentProcess.ProcessName;
            Mutex mutex = new Mutex(true, processName, out bool createNew);
            if (createNew)
            {
                exit = false;
            }
            else
            {
                Process[] processesByName = Process.GetProcessesByName(currentProcess.ProcessName);
                int num = 0;
                while (num < (int)processesByName.Length)
                {
                    Process process = processesByName[num];
                    if (process.Id == currentProcess.Id || !(process.MainWindowHandle != IntPtr.Zero))
                    {
                        num++;
                    }
                    else
                    {
                        WinApiHelper.SetForegroundWindow(process.MainWindowHandle);
                        WinApiHelper.RestoreWindowAsync(process.MainWindowHandle);
                        break;
                    }
                }
                exit = true;
            }
            return mutex;
        }
        private static class WinApiHelper
        {
            [SecuritySafeCritical]
            public static bool IsMaxmimized(IntPtr hwnd)
            {
                WinApiHelper.Import.WINDOWPLACEMENT wINDOWPLACEMENT = new WinApiHelper.Import.WINDOWPLACEMENT();
                if (!WinApiHelper.Import.GetWindowPlacement(hwnd, ref wINDOWPLACEMENT))
                {
                    return false;
                }
                return wINDOWPLACEMENT.showCmd == WinApiHelper.Import.ShowWindowCommands.ShowMaximized;
            }

            [SecuritySafeCritical]
            public static bool RestoreWindowAsync(IntPtr hwnd)
            {
                return WinApiHelper.Import.ShowWindowAsync(hwnd, (WinApiHelper.IsMaxmimized(hwnd) ? 3 : 9));
            }

            [SecuritySafeCritical]
            public static bool SetForegroundWindow(IntPtr hwnd)
            {
                return WinApiHelper.Import.SetForegroundWindow(hwnd);
            }

            private static class Import
            {
                [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
                public static extern bool GetWindowPlacement(IntPtr hWnd, ref WinApiHelper.Import.WINDOWPLACEMENT lpwndpl);

                [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
                public static extern bool SetForegroundWindow(IntPtr hWnd);

                [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
                public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

                public enum ShowWindowCommands
                {
                    Hide,
                    Normal,
                    ShowMinimized,
                    ShowMaximized,
                    ShowNoActivate,
                    Show,
                    Minimize,
                    ShowMinNoActive,
                    ShowNA,
                    Restore,
                    ShowDefault,
                    ForceMinimize
                }

                public struct WINDOWPLACEMENT
                {
                    public int length;

                    public int flags;

                    public WinApiHelper.Import.ShowWindowCommands showCmd;

                    public Point ptMinPosition;

                    public Point ptMaxPosition;

                    public Rectangle rcNormalPosition;
                }
            }
        }
    }

}
