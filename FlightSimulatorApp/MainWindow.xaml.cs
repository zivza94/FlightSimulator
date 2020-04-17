using System;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).loginVM;
            AppView startApp = new AppView();
        }      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // ADD CONNECTION TO THE IP AND PORT
            (Application.Current as App).loginVM.StartConnection();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}
