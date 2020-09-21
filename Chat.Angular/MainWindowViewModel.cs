using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;

namespace Chat.Angular
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string console = string.Empty;
        private string input = string.Empty;

        private ActionCommand launchClientCommand;
        private ActionCommand sendMessageCommand;

        public HubConnection SignalRConnection { get; set; }

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
            launchClientCommand ??= new ActionCommand(_ =>
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "http://localhost:5000",
                    UseShellExecute = true,
                });
            });

        public ActionCommand SendMessageCommand =>
            sendMessageCommand ??= new ActionCommand(async _ =>
            {
                await SignalRConnection.InvokeAsync("Broadcast", "WPF", Input);
                Input = string.Empty;

            });

        public async void ConnectAsync()
        {
            SignalRConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chat")
                .Build();

            SignalRConnection.On<string, string>("Receive", (user, msg) => Console += $"{user}: {msg}\n");

            await SignalRConnection.StartAsync();
        }
    }
}
