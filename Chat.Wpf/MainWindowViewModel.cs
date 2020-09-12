using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Chat.Wpf
{
    internal class MainWindowViewModel : ObservableModel
    {
        private BackgroundWorker worker;

        private string input;
        private string output;

        private ActionCommand runCommand;
        private ActionCommand stopCommand;
        private ActionCommand launchCommand;

        public BackgroundWorker Worker
        {
            get => worker;

            set
            {
                worker = value;
                OnPropertyChanged(nameof(Worker));
            }
        }

        public string Input
        {
            get => input;

            set
            {
                input = value;
                OnPropertyChanged(nameof(Input));
            }
        }

        public string Output
        {
            get => output;

            set
            {
                output = value;
                OnPropertyChanged(nameof(Output));
            }
        }

        public ActionCommand RunCommand =>
            runCommand ??= new ActionCommand(_ =>
            {
                Worker = new BackgroundWorker()
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true,
                };

                Worker.DoWork += Server.Run;

                Worker.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
                    Output = e.UserState is string
                        ? $"{(Output is null ? e.UserState : $"{Output}\n{e.UserState}")}"
                        : throw new InvalidOperationException();

                Worker.RunWorkerAsync(Worker);

            }, _ => Worker is null);

        public ActionCommand StopCommand =>
            stopCommand ??= new ActionCommand(_ =>
            {
                Worker?.CancelAsync();
                Worker = null;

            }, _ => Worker != null);

        public ActionCommand LaunchCommand =>
            launchCommand ??= new ActionCommand(_ =>
            {
                var psi = new ProcessStartInfo()
                {
                    FileName = "index.html",
                    UseShellExecute = true,
                };

                var p = Process.Start(psi);
            });
    }
}
