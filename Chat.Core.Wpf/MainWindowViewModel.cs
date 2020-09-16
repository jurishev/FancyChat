using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;

namespace Chat.Core.Wpf
{
    internal class MainWindowViewModel : ObservableModel
    {
        private string console = string.Empty;
        private string input = string.Empty;

        private ActionCommand runServerCommand;
        private ActionCommand connectCommand;
        private ActionCommand launchClientCommand;
        private ActionCommand sendMessageCommand;

        private bool isWebHostOn;
        private bool isConnected;

        public HubConnection SignalRConnection { get; set; }

        public bool IsWebHostOn
        {
            get => isWebHostOn;

            set
            {
                isWebHostOn = value;
                OnPropertyChanged(nameof(IsWebHostOn));
            }
        }

        public bool IsConnected
        {
            get => isConnected;

            set
            {
                isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

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

        public ActionCommand RunServerCommand =>
            runServerCommand ??= new ActionCommand(_ =>
            {
                if (!isWebHostOn)
                {
                    IsWebHostOn = true;
                    WebHost.RunAsync();
                }
            }, _ => !IsWebHostOn);

        public ActionCommand ConnectCommand =>
            connectCommand ??= new ActionCommand(_ =>
            {
                if (!isConnected)
                {
                    IsConnected = true;
                    ConnectAsync();
                }
            }, _ => IsWebHostOn && !IsConnected);

        public ActionCommand LaunchClientCommand =>
            launchClientCommand ??= new ActionCommand(_ =>
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "http://localhost:5000",
                    UseShellExecute = true,
                });
            }, _ => IsWebHostOn && IsConnected);

        public ActionCommand SendMessageCommand =>
            sendMessageCommand ??= new ActionCommand(async _ =>
            {
                await SignalRConnection.InvokeAsync("Broadcast", "WPF", Input);
                Input = string.Empty;

            }, _ => IsWebHostOn && IsConnected);

        private async void ConnectAsync()
        {
            SignalRConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chat")
                .Build();

            SignalRConnection.On<string, string>("Receive", (user, msg) => Console += $"{user}: {msg}\n");

            await SignalRConnection.StartAsync();
        }
    }
}
