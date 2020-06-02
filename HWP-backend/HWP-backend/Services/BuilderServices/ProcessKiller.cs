using System;
using System.Diagnostics;
using System.Threading;

namespace HWP_backend.Services.BuilderServices
{
    public class ProcessKiller
    {
        private readonly int _delayInSeconds;
        private Timer _timer;

        public ProcessKiller()
        {
            _delayInSeconds = 3;
        }


        public void KillProcess(Process process)
        {
            var killTimer = new TimerCallback(Kill);
            _timer = new Timer(killTimer, process, _delayInSeconds * 1000, -1);
        }

        private void Kill(object process)
        {
            var p = process as Process;
            try
            {
                if (p != null && p.HasExited) return;
                p.Kill();
                _timer.Dispose();
                Console.WriteLine("Process was killed");
            }
            catch (Exception)
            {
                Console.WriteLine("Process stopped by itself");
            }
        }
    }
}