using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("VM_" + e.PropertyName); };
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
            get { return _model.Latitude; }
        }
        public double VM_Longitude
        {
            get { return _model.Longitude; }
        }
    }
}
