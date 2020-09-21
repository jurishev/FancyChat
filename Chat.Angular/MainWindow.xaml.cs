using System.ComponentModel;
using System.Windows;

namespace Chat.Angular
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.ConnectAsync();
        }

        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            var connection = (DataContext as MainWindowViewModel)?.SignalRConnection;

            if (connection != null)
            {
                await connection.StopAsync();
                await connection.DisposeAsync();
            }
        }
    }
}
