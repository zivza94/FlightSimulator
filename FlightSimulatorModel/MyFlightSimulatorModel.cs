using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contracts;

namespace FlightSimulatorModel
{
    public class MyFlightSimulatorModel: IFlightSimulatorModel , INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ITelnetClient _telnetClient;
        bool _stop;
        double _heading;
        double _varSpeed;
        double _groundSpeed;
        double _airSpeed;
        double _gpsAltitude;
        double _roll;
        double _pitch;
        double _altAltitude;
        public MyFlightSimulatorModel(ITelnetClient telnetClient)
        {
            _telnetClient = telnetClient;
            _stop = false;
        }

        public Double Heading {
            get { return _heading; }
            set {
                _heading = value;
                NotifyPropertyChanged("Heading");
            }
        }
        public Double VerSpeed {
            get { return _varSpeed; }
            set
            {
                _varSpeed = value;
                NotifyPropertyChanged("VarSpeed");
            }
        }
        public Double GroundSpeed {
            get { return _groundSpeed; }
            set
            {
                _groundSpeed = value;
                NotifyPropertyChanged("GroundSpeed");
            }
        }
        public Double AirSpeed {
            get { return _airSpeed; }
            set
            {
                _airSpeed = value;
                NotifyPropertyChanged("AirSpeed");
            }
        }
        public Double GpsAltitude {
            get { return _gpsAltitude ; }
            set
            {
                _gpsAltitude = value;
                NotifyPropertyChanged("GpsAltitude");
            }
        }
        public Double Roll {
            get { return _roll; }
            set
            {
                _roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        public Double Pitch {
            get { return _pitch; }
            set
            {
                _pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        public Double AltAltitude {
            get { return _altAltitude; }
            set
            {
                _altAltitude = value;
                NotifyPropertyChanged("AltAltitude");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void Connect(string ip, int port)
        {
            _telnetClient.Connect(ip, port);          
        }


        public void Disconnect()
        {
            _stop = true;
            _telnetClient.Disconnect();
        }        
        public void Start()
        {

            new Thread(delegate ()
            {
                while (!_stop)
                {
                    WriteAndRead();
                }
            });
        }
        
        private void WriteAndRead()
        {
            Heading = WriteToSimulator("get indicated-heading-deg");
            VerSpeed = WriteToSimulator("get gps_indicated-vertical-speed");
            GroundSpeed = WriteToSimulator("get gps_indicated-ground-speed-kt");
            AirSpeed = WriteToSimulator("get airspeed-indicator_indicated-speed-kt ");
            GpsAltitude = WriteToSimulator("get gps_indicated-altitude-ft");
            Roll = WriteToSimulator("get attitude-indicator_internal-roll-deg");
            Pitch = WriteToSimulator("get attitude-indicator_internal-pitch-deg");
            AltAltitude = WriteToSimulator("get altimeter_indicated-altitude-ft");
        }

        private double WriteToSimulator(string command)
        {
            double retval;
            _telnetClient.Write(command);
            retval = Double.Parse(_telnetClient.Read());
            return retval;
        }
    }
}
