using FlightSimulatorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Default
        int intPort = 0;
        int intIp = 0;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).loginVM;
        }

        private void TextBoxCheck(TextBox textBox)
        {
            string strNum = textBox.Text;
            int num = 0;
            if (strNum.Equals("Default")) { }
            else if (strNum.Length > 0 && int.TryParse(strNum, out num) == false)
            {
                MessageBox.Show("Use only numbers");
                textBox.Text = textBox.Text.Remove(strNum.Length - 1);
            } else
            {
                if (textBox.Name.Equals("port"))
                {
                    intPort = num;
                }
                else
                {
                    intIp = num;
                }
            }
        }
        private void TextBox_TextChanged_PORT(object sender, TextChangedEventArgs e)
        {
        }

        private void TextBox_TextChanged_IP(object sender, TextChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // ADD CONNECTION TO THE IP AND PORT
            (Application.Current as App).loginVM.StartConnection();
            AppView startApp = new AppView();
            this.Hide();
            startApp.Show();
        }
    }
}
