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
    public class FlightSimulatorModel: IFlightSimulatorModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        IClient _client;
        bool _stop;
        double _heading;
        double _varSpeed;
        double _groundSpeed;
        double _airSpeed;
        double _gpsAltitude;
        double _roll;
        double _pitch;
        double _altAltitude;
        double _latitude;
        double _longitude;
        public FlightSimulatorModel(IClient client)
        {
            _client = client;
            _stop = false;
        }

        public Double Heading {
            get { return _heading; }
            set {
                if (!_heading.Equals(value))
                {
                    _heading = value;
                    NotifyPropertyChanged("Heading");
                }
                
            }
        }
        public Double VerSpeed {
            get { return _varSpeed; }
            set
            {
                if (!_varSpeed.Equals(value))
                {
                    _varSpeed = value;
                    NotifyPropertyChanged("VarSpeed");
                }
                
            }
        }
        public Double GroundSpeed {
            get { return _groundSpeed; }
            set
            {
                if (!_groundSpeed.Equals(value))
                {
                    _groundSpeed = value;
                    NotifyPropertyChanged("GroundSpeed");
                }
                
            }
        }
        public Double AirSpeed {
            get { return _airSpeed; }
            set
            {
                if (!_airSpeed.Equals(value))
                {
                    _airSpeed = value;
                    NotifyPropertyChanged("AirSpeed");
                }
                
            }
        }
        public Double GpsAltitude {
            get { return _gpsAltitude ; }
            set
            {
                if (!_gpsAltitude.Equals(value))
                {
                    _gpsAltitude = value;
                    NotifyPropertyChanged("GpsAltitude");
                }
                
            }
        }
        public Double Roll {
            get { return _roll; }
            set
            {
                if (!_roll.Equals(value))
                {
                    _roll = value;
                    NotifyPropertyChanged("Roll");
                }
                
            }
        }
        public Double Pitch {
            get { return _pitch; }
            set
            {
                if (!_pitch.Equals(value))
                {
                    _pitch = value;
                    NotifyPropertyChanged("Pitch");
                }
                
            }
        }
        public Double AltAltitude {
            get { return _altAltitude; }
            set
            {
                if (!_altAltitude.Equals(value))
                {
                    _altAltitude = value;
                    NotifyPropertyChanged("AltAltitude");
                }
                
            }
        }
        public Double Latitude
        {
            get { return _latitude; }
            set
            {
                if (!_latitude.Equals(value))
                {
                    _latitude = value;
                    NotifyPropertyChanged("Latitude");
                }
                
            }
        }
        public Double Longitude
        {
            get { return _longitude; }
            set
            {
                if (!_longitude.Equals(value))
                {
                    _longitude = value;
                    NotifyPropertyChanged("Longitude");
                }
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
            _client.Connect(ip, port);
            Console.WriteLine("connected to ip {0} and port {1}", ip,port);
        }


        public void Disconnect()
        {
            _stop = true;
            _client.Disconnect();
            Console.WriteLine("disconnected to server");
        }        
        public void Start()
        {
            new Thread(delegate ()
            {
                while (!_stop)
                {
                    //Console.WriteLine("start");
                    WriteAndRead();
                    Thread.Sleep(1000);
                }
            }).Start();
        }
        
        private void WriteAndRead()
        {
            //Console.WriteLine("write + read");
            Heading = WriteToSimulator("get /instrumentation/heading-indicator/indicated-heading-deg \n",Heading);
            VerSpeed = WriteToSimulator("get /instrumentation/gps/indicated-vertical-speed \n",VerSpeed);
            GroundSpeed = WriteToSimulator("get /instrumentation/gps/indicated-ground-speed-kt \n",GroundSpeed);
            AirSpeed = WriteToSimulator("get /instrumentation/airspeed-indicator/indicated-speed-kt \n",AirSpeed);
            GpsAltitude = WriteToSimulator("get /instrumentation/gps/indicated-altitude-ft \n", GpsAltitude);
            Roll = WriteToSimulator("get /instrumentation/attitude-indicator/internal-roll-deg \n",Roll);
            Pitch = WriteToSimulator("get /instrumentation/attitude-indicator/internal-pitch-deg \n",Pitch);
            AltAltitude = WriteToSimulator("get /instrumentation/altimeter/indicated-altitude-ft \n",AltAltitude);
            Latitude = WriteToSimulator("get /position/latitude-deg \n",Latitude);
            Longitude = WriteToSimulator("get  / position/longitude-deg \n", Longitude);
        }
        public void SetRudderAndElevator(double rudder, double elevator)
        {
            WriteToSimulator("set /controls/flight/rudder "+ rudder + "\n",rudder);
            WriteToSimulator("set /controls/flight/elevator " + elevator + "\n",elevator);

        }

        public void SetAileron(double aileron)
        {
            Console.WriteLine("send aileron");
            WriteToSimulator("set /controls/flight/aileron " + aileron + "\n",aileron);
        }

        public void SetThrottle(double throttle)
        {
            Console.WriteLine("send throttle");
            WriteToSimulator("set /controls/engines/engine/throttle " + throttle + "\n",throttle);
        }

        private double WriteToSimulator(string command, Double prop)
        {
            double retval;
            _client.Write(command);
            string value = _client.Read();
            if (!Double.TryParse(value, out retval))
            {
                retval = prop;
            }
            return retval;
        }
    }
}
