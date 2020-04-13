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
        private Point knobCenter;
        private bool mousePressed;
        //members for the Animation
        private Storyboard centerKnobStoryBoard;
        private DoubleAnimation x, y;
        private double x2, y2;
        public Joystick()
        {
            InitializeComponent();
            mousePressed = false;
            x2 = 0;
            y2 = 0;
            knobCenter = new Point(Base.Width / 2, Base.Height / 2);
            centerKnobStoryBoard = Knob.Resources["CenterKnob"] as Storyboard;
            x = centerKnobStoryBoard.Children[0] as DoubleAnimation;
            y = centerKnobStoryBoard.Children[1] as DoubleAnimation;
            x.From = 0;
            y.From = 0;
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

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                x2 = e.GetPosition(Base).X;
                y2 = e.GetPosition(Base).Y;
                //cheak if the knobBase is in Bound
                double bound = Math.Sqrt(Math.Pow(x2 - knobCenter.X, 2) + Math.Pow(y2 - knobCenter.Y, 2));
                if (bound > (this.Base.Width / 2) - (KnobBase.Width / 2) || bound > (this.Base.Height / 2) - (KnobBase.Height / 2))
                {
                    return;
                }
                else
                {
                    x.To = x2 - knobCenter.X;
                    y.To = y2 - knobCenter.Y;
                    //start the animation
                    centerKnobStoryBoard.Begin();
                    x.From = x.To;
                    y.From = y.To;
                }
            }
        }

        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mousePressed = true;
            Knob.CaptureMouse();
        }

        private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mousePressed = false;
            Knob.ReleaseMouseCapture();
            //Return the animation to the middle
            x.To = 0;
            y.To = 0;
            //start the animation
            centerKnobStoryBoard.Begin();
            x.From = x.To;
            y.From = y.To;
        }
    }
}
