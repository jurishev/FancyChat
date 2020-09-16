using System.Windows;
using System.ComponentModel;

namespace Chat.Core.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }

        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            var connection = (DataContext as MainWindowViewModel)?.SignalRConnection;

            if (connection != null)
            {
                await connection.StopAsync();
                await connection.DisposeAsync();
            }

            WebHost.Stop();
            WebHost.Dispose();
        }
    }
}
