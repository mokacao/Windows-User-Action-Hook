﻿using System;
using System.Diagnostics;
using System.Text;
using EventHook.Hooks.Library;

namespace EventHook.Hooks.Helpers
{
    public class WindowHelper
    {
        public static IntPtr GetActiveWindowHandle()
        {
            try
            {
                return (IntPtr) User32.GetForegroundWindow();
            }
            catch (Exception)
            {
                // ignored
            }
            return IntPtr.Zero;
        }

        public static string GetAppPath(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) return null;
            try
            {
                uint pid;
                User32.GetWindowThreadProcessId(hWnd, out pid);
                var proc = Process.GetProcessById((int) pid);
                return proc.MainModule.FileName;
            }
            catch
            {
                return null;
            }
        }

        public static string GetWindowText(IntPtr hWnd)
        {
            try
            {
                var length = User32.GetWindowTextLength(hWnd);
                var sb = new StringBuilder(length + 1);
                User32.GetWindowText(hWnd, sb, sb.Capacity);
                return sb.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetAppDescription(string appPath)
        {
            if (appPath == null) return null;
            try
            {
                return FileVersionInfo.GetVersionInfo(appPath).FileDescription;
            }
            catch
            {
                return null;
            }
        }
    }
}