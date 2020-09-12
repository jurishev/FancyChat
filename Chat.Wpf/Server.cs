using System;
using System.ComponentModel;
using System.Threading;

namespace Chat.Wpf
{
    internal static class Server
    {
        public static void Run(object sender, DoWorkEventArgs e)
        {
            if (!(e.Argument is BackgroundWorker worker))
            {
                throw new ArgumentException(nameof(e.Argument));
            }

            var i = 1;

            while (!worker.CancellationPending)
            {
                worker.ReportProgress(0, $"{i++}");
                Thread.Sleep(1000);
            }

            e.Cancel = true;
        }
    }
}
