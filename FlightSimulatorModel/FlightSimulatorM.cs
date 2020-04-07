using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contracts;

namespace FlightSimulatorModel
{
    public class FlightSimulatorM: IFlightSimulatorModel
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
        string _logger;
        bool _connected = false;
        string _ip = System.Configuration.ConfigurationManager.AppSettings["ip"].ToString();
        string _port = ConfigurationManager.AppSettings["port"].ToString();

        public FlightSimulatorM(IClient client)
        {
            _client = client;
            _stop = false;
        }
        //DashBoard
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
        //Map
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
        //Login
        public string Logger
        {
            get
            {
                return _logger;
            }                                                
        }
        public bool Connected { 
            get 
            {
                return _connected;
            }
            set {
                if(_connected != value)
                {
                    _connected = value;
                    NotifyPropertyChanged("Connected");
                }              
            }
        }
        public string Port
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
        public string Ip
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
            }
        }



        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void Connect()
        {
            int portNum;
            if(!int.TryParse(_port, out portNum))
            {
                AddToLogger("Port number is numbers only, please try again");
                return;
            }
            try
            {
                _client.Connect(_ip, portNum);
                AddToLogger("connected to ip " +_ip+ " and port "+ portNum);
                Connected = true;
            }catch(Exception e)
            {
                AddToLogger("Failed to connect to server, Please try again" + e.Message);
            }
            
        }


        public void Disconnect()
        {
            _stop = true;
            try
            {
                _client.Disconnect();
                Connected = false;
            }
            catch (Exception e)
            {
                AddToLogger("Failed to disconnect from server" + e.Message);
            }
        }        
        public void Start()
        {
            new Thread(delegate ()
            {
                while (!_stop)
                {
                    WriteAndRead();
                    Thread.Sleep(250);
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
            if (Latitude > 180)
            {
                Latitude = 180;
                AddToLogger("Latitude out of boundaries");
            }
            else if (Latitude < -180)
            {
                Latitude = -180;
                AddToLogger("Latitude out of boundaries");
            }
            Longitude = WriteToSimulator("get  / position/longitude-deg \n", Longitude);
            if(Longitude > 90)
            {
                Longitude = 90;
                AddToLogger("Longtitude out of boundaries");
            } 
            else if (Longitude < -90)
            {
                Longitude = -90;
                AddToLogger("Longtitude out of boundaries");
            }
        }
        public void SetRudderAndElevator(double rudder, double elevator)
        {
                WriteToSimulator("set /controls/flight/rudder " + rudder + "\n", rudder);
                WriteToSimulator("set /controls/flight/elevator " + elevator + "\n", elevator);
        }

        public void SetAileron(double aileron)
        {
                WriteToSimulator("set /controls/flight/aileron " + aileron + "\n", aileron);
        }

        public void SetThrottle(double throttle)
        {           
            WriteToSimulator("set /controls/engines/engine/throttle " + throttle + "\n",throttle);
        }

        private double WriteToSimulator(string command, Double prop)
        {
            double retval;
            try
            {
                _client.Write(command);
                string value = _client.Read();
                if (!Double.TryParse(value, out retval))
                {
                    retval = prop;
                }

            }
            catch(Exception ex)
            {
                AddToLogger(ex.Message);
                retval = prop;
            }
            
            return retval;
        }
        public void AddToLogger(string msg)
        {
            _logger += msg + "\n";
            NotifyPropertyChanged("Logger");
        }
    }
}
