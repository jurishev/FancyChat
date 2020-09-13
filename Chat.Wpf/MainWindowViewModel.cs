using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Chat.Wpf
{
    internal class MainWindowViewModel : ObservableModel
    {
        private readonly string html;

        private string input;
        private string output;

        private ActionCommand launchCommand;

        public MainWindowViewModel() => html = File.ReadAllText("index.html");

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

        public ActionCommand LaunchCommand =>
            launchCommand ??= new ActionCommand(_ => Listen());

        private async void Listen()
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:4000/");
            listener.Start();

            Process.Start(new ProcessStartInfo()
            {
                FileName = "http://localhost:4000/",
                UseShellExecute = true,
            });

            var httpContext = await listener.GetContextAsync();
            var response = httpContext.Response;

            byte[] buff = Encoding.UTF8.GetBytes(html);
            response.ContentLength64 = buff.Length;
            response.OutputStream.Write(buff, 0, buff.Length);
            response.OutputStream.Close();

            listener.Stop();
        }
    }
}
