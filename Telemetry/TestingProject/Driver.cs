namespace TestingProject
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;


    public class DriverConfiguration
    {
        public ushort KeyAccelerate { get; set; }
        public ushort KeyDecelerate { get; set; }
        public ushort KeyShiftUp { get; set; }
        public ushort KeyShiftDown { get; set; }
    }

    public class Driver
    {
        public Driver(Process process, DriverConfiguration configuration)
        {
            Configuration = configuration;
            Process = process;
        }

        protected readonly DriverConfiguration Configuration;
        protected readonly Process Process;

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("InputHook.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void KeyPress(ushort hexKey);

        public void Accelerate()
        {
            SetForegroundWindow(Process.MainWindowHandle);
            Thread.Sleep(100);
            KeyPress(Configuration.KeyAccelerate);
        }

        public void Decelerate()
        {
            SetForegroundWindow(Process.MainWindowHandle);
            Thread.Sleep(100);
            KeyPress(Configuration.KeyDecelerate);
        }

        public void ShiftUp()
        {
            SetForegroundWindow(Process.MainWindowHandle);
            Thread.Sleep(100);
            KeyPress(Configuration.KeyShiftUp);
        }

        public void ShiftDown()
        {
            SetForegroundWindow(Process.MainWindowHandle);
            Thread.Sleep(100);
            KeyPress(Configuration.KeyShiftDown);
        }
    }
}