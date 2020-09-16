using System.Windows;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.ConnectAsync();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await (DataContext as MainWindowViewModel)?.Connection.DisposeAsync();
        }
    }
}
