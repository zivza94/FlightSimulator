using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorModel
{
    public class FlightSimulatorDashboardVM:INotifyPropertyChanged
    {
        readonly private IFlightSimulatorModel _model;
        public event PropertyChangedEventHandler PropertyChanged;

        public FlightSimulatorDashboardVM(IFlightSimulatorModel model)
        {
            _model = model;
            _model.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
        }
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
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
    }
}
