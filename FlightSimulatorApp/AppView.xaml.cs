using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for AppView.xaml
    /// </summary>
    public partial class AppView : Window
    {
        public AppView()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).loginVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).loginVM.EndConnection();
        }
    }
}
