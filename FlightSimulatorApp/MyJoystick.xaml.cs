using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MyJoystick.xaml
    /// </summary>
    public partial class MyJoystick : UserControl
    {
        private Point knobCenter;
        private bool mousePressed, xMax, yMax;
        //members for the Animation
        protected Storyboard centerKnobStoryBoard;
        private DoubleAnimation x, y;
        private double x2, y2, rudder, elevator;
        
        private FlightSimulatorModel.FlightSimulatorControlVM controlVM;
        public MyJoystick()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).controlVM;
            controlVM = DataContext as FlightSimulatorModel.FlightSimulatorControlVM;
            mousePressed = false;
            x2 = 0;
            y2 = 0;
            knobCenter = new Point(joystick.Base.Width / 2, joystick.Base.Height / 2);
            centerKnobStoryBoard = joystick.Knob.Resources["CenterKnob"] as Storyboard;
            x = centerKnobStoryBoard.Children[0] as DoubleAnimation;
            y = centerKnobStoryBoard.Children[1] as DoubleAnimation;
            x.From = 0;
            y.From = 0;
            rudder = 0;
            elevator = 0;
        }
        private void joystick_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                x2 = e.GetPosition(joystick.Base).X;
                y2 = e.GetPosition(joystick.Base).Y;
                double bound = Math.Sqrt(Math.Pow(x2 - knobCenter.X, 2) + Math.Pow(y2 - knobCenter.Y, 2));
                //check if the knobBase is in Bound
                if (bound > (joystick.Base.Width / 4) - (joystick.KnobBase.Width / 2))
                {
                    xMax = true;
                } else
                {
                    xMax = false;
                }
                if (bound > (joystick.Base.Height / 4) - (joystick.KnobBase.Height / 2))
                {
                    yMax = true;
                }
                else
                {
                    yMax = false;
                }
                if (xMax && yMax)
                {
                    return;
                }
                if (xMax == false)
                {
                    x.To = x2 - knobCenter.X;
                }
                if (yMax == false)
                {
                    y.To = y2 - knobCenter.Y;

                }
                //start the animation          
                centerKnobStoryBoard.Begin();
                UpdateRudderAndElevator((double)x.To, -(double)y.To);
                if (xMax == false)
                {
                    x.From = x.To;
                }
                if (yMax == false)
                {
                    y.From = y.To;
                }
               
            }
        }

        private void Joystick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mousePressed = true;
            joystick.Knob.CaptureMouse();
        }

        private void joystick_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            centerKnobStoryBoard = joystick.Knob.Resources["CenterKnob"] as Storyboard;
            mousePressed = false;
            joystick.Knob.ReleaseMouseCapture();
            //Return the animation to the middle
            x.To = 0;
            y.To = 0;
            //start the animation"
            //x.BeginAnimation();
            //Duration = new System.Windows.Duration(new TimeSpan(40000000));
            //y.BeginAnimation();
            x.Duration = new System.Windows.Duration(new TimeSpan(20000000));
            y.Duration = new Duration(new TimeSpan(20000000));
            centerKnobStoryBoard.Begin();
            UpdateRudderAndElevator(0, 0);
            x.From = x.To;
            y.From = y.To;
        }

        private void UpdateRudderAndElevator(double x, double y)
        {
            rudder = x / (joystick.KnobBase.Width / 2.8);
            elevator = y / (joystick.KnobBase.Height / 2.8);
            if (rudder > 1)
            {
                rudder = 1;
            }
            if (rudder < -1)
            {
                rudder = -1;
            }
            if (elevator > 1)
            {
                elevator = 1;
            }
            if (elevator < -1)
            {
                elevator = -1;
            }
            controlVM.SetRudderAndElevator(rudder, elevator);
            Rudder.Text = rudder.ToString("0.000");
            Elevator.Content = elevator.ToString("0.000");
        }
    }
}
