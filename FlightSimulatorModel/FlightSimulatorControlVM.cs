﻿using Contracts;
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
    public class FlightSimulatorControlVM : INotifyPropertyChanged
    {
        readonly private IFlightSimulatorModel _model;
        public event PropertyChangedEventHandler PropertyChanged;
        private double _aileron;
        private double _throttle;
        public FlightSimulatorControlVM(IFlightSimulatorModel model)
        {
            _model = model;
            _model.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
            

        }

        
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
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