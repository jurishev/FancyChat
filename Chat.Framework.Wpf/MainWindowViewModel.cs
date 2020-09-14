using System.Diagnostics;
using Microsoft.AspNet.SignalR.Client;

namespace Chat.Framework.Wpf
{
    internal class MainWindowViewModel : ObservableModel
    {
        private IHubProxy proxy;

        private string console = string.Empty;
        private string input;

        private ActionCommand launchClientCommand;
        private ActionCommand sendMessageCommand;

        public HubConnection Connection { get; set; }

        public string Console
        {
            get => console;

            set
            {
                console = value;
                OnPropertyChanged(nameof(Console));
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

        public ActionCommand LaunchClientCommand =>
            launchClientCommand ?? (launchClientCommand = new ActionCommand(_ =>
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "index.html",
                    UseShellExecute = true,
                });
            }));

        public ActionCommand SendMessageCommand =>
            sendMessageCommand ?? (sendMessageCommand = new ActionCommand(_ =>
            {
                proxy.Invoke("Broadcast", "WPF", Input);
                Input = string.Empty;
            }));

        public async void ConnectAsync()
        {
            Connection = new HubConnection("http://localhost:4000/signalr");

            proxy = Connection.CreateHubProxy("ChatHub");
            proxy.On<string, string>("ReceiveMessage", (name, msg) => Console += $"{name}: {msg}\n");

            await Connection.Start();
        }
    }
}
