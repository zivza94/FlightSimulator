using Contracts;
using FlightSimulatorModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorModel
{
    public class MyFlightSimulatorViewModel : INotifyPropertyChanged, IFlightSimulatorViewModel
    {
        readonly private IFlightSimulatorModel _model;
        public event PropertyChangedEventHandler PropertyChanged;
        private double _aileron;
        private double _throttle;
        public MyFlightSimulatorViewModel(IFlightSimulatorModel model)
        {
            _model = model;
            _model.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
            

        }

        public void EndConnection()
        {
            _model.Disconnect();
        }
        public void StartConnection()
        {
            _model.Connect("localhost", 5402);
            _model.Start();
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public double VM_Heading
        {
            get { return _model.Heading; }
        }
        public double VM_VerSpeed
        {
            get { return _model.VerSpeed; }
        }
        public double VM_GroundSpeed
        {
            get { return _model.GroundSpeed; }
        }
        public double VM_AirSpeed
        {
            get { return _model.AirSpeed; }
        }
        public double VM_GpsAltitude
        {
            get { return _model.GpsAltitude; }
        }
        public double VM_Roll
        {
            get { return _model.Roll; }
        }
        public double VM_Pitch
        {
            get { return _model.Pitch; }
        }
        public double VM_AltAltitude
        {
            get { return _model.AltAltitude; }
        }
        public double VM_Latitude
        {
            get { return _model.Latitude; }
        }
        public double VM_Longitude
        {
            get { return _model.Longitude; }
        }
        public double VM_Throttle
        {
            get { return _throttle; }
            set
            {
                _throttle = value;
                _model.SetThrottle(_throttle);
            }
        }
        public double VM_Aileron
        {
            get { return _aileron; }
            set
            {
                _aileron = value;
                _model.SetAileron(_aileron);
            }
        }
        public void SetRudderAndElevator(double rudder, double elevator)
        {
            _model.SetRudderAndElevator(rudder, elevator);
        }
    }
}
