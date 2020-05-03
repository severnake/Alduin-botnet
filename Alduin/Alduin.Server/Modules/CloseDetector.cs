using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Server.Modules
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class CloseDetector
    {
        [DllImport("kernel32.dll")]
        static extern int SetConsoleCtrlHandler(ControlEventHandler HandlerRoutine, bool Add);

        public enum ConsoleEvent
        {
            CTRL_C = 0,
            CTRL_BREAK = 1,
            CTRL_CLOSE = 2,
            CTRL_LOGOFF = 5,
            CTRL_SHUTDOWN = 6
        }

        public delegate void ControlEventHandler(ConsoleEvent consoleEvent);

        public static void OnControlEvent(ConsoleEvent consoleEvent)
        {
            // Control-C doesn't debug well
            Interlocked.Decrement(ref isRunning);
            Console.WriteLine("Event: {0}", consoleEvent);
            Console.WriteLine("Thread closed gracefully");
            ConfigTor.KillTor();
            Thread.Sleep(500);
        }

        private static Thread t = new Thread(looping);
        private static long isRunning;

        public static void Detecter()
        {
            SetConsoleCtrlHandler(new ControlEventHandler(OnControlEvent), true);
            isRunning = 1L;
            t.Start();
            t.Join();
        }

        public static void looping()
        {
            do
                Thread.Sleep(500);
            while (Interlocked.Read(ref isRunning) > 0L);
        }
    }
}
