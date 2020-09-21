using System.Windows;

namespace Chat.Angular
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            WebHost.RunAsync();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            WebHost.Stop();
            WebHost.Dispose();
        }
    }
}
