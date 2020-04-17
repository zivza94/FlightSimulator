using Contracts;
using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;

namespace FlightSimulatorModel
{
    public class FlightSimulatorMapVM:INotifyPropertyChanged
    {
        readonly private IFlightSimulatorModel _model;
        public event PropertyChangedEventHandler PropertyChanged;

        public FlightSimulatorMapVM(IFlightSimulatorModel model)
        {
            _model = model;
            _model.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_Location"); };
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public double VM_Latitude
        {
            get { return _model.Latitude;}
        }
        public double VM_Longitude
        {
            get { return _model.Longitude; }
        }
        public Location VM_Location
        {
            get { return new Location(VM_Latitude, VM_Longitude); }
        }
    }
}
