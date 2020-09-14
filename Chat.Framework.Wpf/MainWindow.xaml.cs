using System.Windows;

namespace Chat.Framework.Wpf
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.Connection.Dispose();
        }
    }
}
