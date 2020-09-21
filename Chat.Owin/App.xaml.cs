using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Windows;

namespace Chat.Owin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread()]
        public static void Main()
        {
            using (WebApp.Start("http://localhost:4000/"))
            {
                App app = new App();
                app.InitializeComponent();
                app.Run();
            }
        }
    }

    public class ChatHub : Hub
    {
        public void Broadcast(string name, string msg) => Clients.All.receiveMessage(name, msg);
    }

    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
