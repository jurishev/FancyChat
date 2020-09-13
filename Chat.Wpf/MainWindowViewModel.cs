using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace Chat.Wpf
{
    internal class MainWindowViewModel : ObservableModel
    {
        private readonly string html;
        private readonly HttpListener listener;

        private string input;
        private string output;

        private ActionCommand startListenerCommand;
        private ActionCommand stopListenerCommand;
        private ActionCommand launchClientCommand;
        private ActionCommand sendMessageCommand;

        private readonly ConcurrentQueue<string> outputQueue = new ConcurrentQueue<string>();
        private readonly ConcurrentBag<WebSocket> aliveSockets = new ConcurrentBag<WebSocket>();
        private readonly ConcurrentBag<WebSocket> deadSockets = new ConcurrentBag<WebSocket>();

        public MainWindowViewModel()
        {
            html = File.ReadAllText("index.html");

            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:4000/");
        }

        public bool ListenerIsListening => listener.IsListening;

        public int AliveSocketCount => aliveSockets.Count;

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

        public ActionCommand StartListenerCommand =>
            startListenerCommand ??= new ActionCommand(_ =>
            {
                listener.Start();
                OnPropertyChanged(nameof(ListenerIsListening));

            }, _ => !ListenerIsListening);

        public ActionCommand StopListenerCommand =>
            stopListenerCommand ??= new ActionCommand(_ =>
            {
                listener.Stop();
                OnPropertyChanged(nameof(ListenerIsListening));

            }, _ => ListenerIsListening);

        public ActionCommand LaunchClientCommand =>
            launchClientCommand ??= new ActionCommand(_ => LaunchClient(), _ => ListenerIsListening);

        public ActionCommand SendMessageCommand =>
            sendMessageCommand ??= new ActionCommand(_ =>
            {
                outputQueue.Enqueue(Input);
                Input = string.Empty;

            }, _ => !string.IsNullOrWhiteSpace(Input));

        private async void LaunchClient()
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = "http://localhost:4000/",
                UseShellExecute = true,
            });

            var context = await listener.GetContextAsync();
            var rsp = context.Response;

            byte[] buff = Encoding.UTF8.GetBytes(html);
            rsp.ContentLength64 = buff.Length;
            rsp.OutputStream.Write(buff, 0, buff.Length);
            rsp.OutputStream.Close();

            context = await listener.GetContextAsync();

            if (context.Request.IsWebSocketRequest)
            {
                var wsc = await context.AcceptWebSocketAsync(null);
                aliveSockets.Add(wsc.WebSocket);

                OnPropertyChanged(nameof(AliveSocketCount));
            }
        }
    }
}
