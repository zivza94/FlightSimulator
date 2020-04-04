using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorModel
{
    public class FlightSimulatorLoginVM
    {
        readonly private IFlightSimulatorModel _model;
        public event PropertyChangedEventHandler PropertyChanged;
        string _ip = "localhost";
        string _port = "5402";
        public string VM_Ip { 
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
            }
        }

        public string VM_Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }
        public bool VM_Connected
        {
            get
            {
                return _model.Connected;
            }
        }

        public string VM_Logger
        {
            get
            {
                return _model.Logger;
            }
        }
        public FlightSimulatorLoginVM(IFlightSimulatorModel model)
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

        public void EndConnection()
        {
            _model.Disconnect();
        }
        public void StartConnection()
        {
            _model.Connect(VM_Ip, VM_Port);
            _model.Start();
        }

    }
}
