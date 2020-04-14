using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.controls
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        public Joystick()
        {
            InitializeComponent();
        }

        public double xConverted
        {
            get
            {
                return (double)GetValue(xProperty);
            }
            set => SetValue(xProperty, value);
        }

        public static readonly DependencyProperty xProperty =
            DependencyProperty.Register(
                "X",
                typeof(DoubleAnimation),
                typeof(Storyboard),
                null);


        private void centerKnob_Completed(object sender, EventArgs e)
        {

        }

    }
}
