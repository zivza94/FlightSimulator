using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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
