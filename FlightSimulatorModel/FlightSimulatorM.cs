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
        ValueValidator _validator;
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
        string _ip = ConfigurationManager.AppSettings["ip"].ToString();
        string _port = ConfigurationManager.AppSettings["port"].ToString();

        public FlightSimulatorM(IClient client)
        {
            _client = client;
            _stop = false;
             _validator = new ValueValidator();
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
                    NotifyPropertyChanged("VerSpeed");
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
            if(!int.TryParse(Port, out portNum))
            {
                AddToLogger("Port number is numbers only, please try again");
                return;
            }
            try
            {
                _client.Connect(Ip, portNum);
                AddToLogger("connected to ip " +Ip+ " and port "+ portNum);
                Connected = true;
            }catch(Exception e)
            {
                AddToLogger("Failed to connect to server, Please check server and try again " + e.Message);
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
            ClearLogger();
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
            Heading = _validator.GetValue("Heading", WriteToSimulator("get /instrumentation/heading-indicator/indicated-heading-deg \n", Heading));
            VerSpeed = _validator.GetValue("VerSpeed", WriteToSimulator("get /instrumentation/gps/indicated-vertical-speed \n", VerSpeed));
            GroundSpeed = _validator.GetValue("GroundSpeed", WriteToSimulator("get /instrumentation/gps/indicated-ground-speed-kt \n",GroundSpeed));
            AirSpeed = _validator.GetValue("AirSpeed", WriteToSimulator("get /instrumentation/airspeed-indicator/indicated-speed-kt \n",AirSpeed));
            GpsAltitude = _validator.GetValue("GpsAltitude", WriteToSimulator("get /instrumentation/gps/indicated-altitude-ft \n", GpsAltitude));
            Roll = _validator.GetValue("Roll", WriteToSimulator("get /instrumentation/attitude-indicator/internal-roll-deg \n",Roll));
            Pitch = _validator.GetValue("Pitch", WriteToSimulator("get /instrumentation/attitude-indicator/internal-pitch-deg \n",Pitch));
            AltAltitude = _validator.GetValue("AltAltitude", WriteToSimulator("get /instrumentation/altimeter/indicated-altitude-ft \n",AltAltitude));
            Latitude = _validator.GetValue("Latitude", WriteToSimulator("get /position/latitude-deg \n",Latitude));
            if(Latitude >= 90 || Latitude <= -90)
            {
                AddToLogger("the plane is out of boundries");
            }
            Longitude = _validator.GetValue("Longitude", WriteToSimulator("get  /position/longitude-deg \n", Longitude));
            if (Longitude >= 180 || Longitude <= -180)
            {
                AddToLogger("the plane is out of boundries");
            }
        }
        public void SetRudderAndElevator(double rudder, double elevator)
        {
            rudder = _validator.GetValue("Rudder", rudder);
            elevator = _validator.GetValue("Elevator", elevator);
            WriteToSimulator("set /controls/flight/rudder " + rudder + "\n", rudder);
            WriteToSimulator("set /controls/flight/elevator " + elevator + "\n", elevator);
        }

        public void SetAileron(double aileron)
        {
            aileron = _validator.GetValue("Aileron", aileron);
            WriteToSimulator("set /controls/flight/aileron " + aileron + "\n", aileron);
        }

        public void SetThrottle(double throttle)
        {
            throttle = _validator.GetValue("Throttle", throttle);
            WriteToSimulator("set /controls/engines/current-engine/throttle " + throttle + "\n",throttle);
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
            catch(TimeoutException ex)
            {
                AddToLogger("timeout period has passed");
                retval = prop;
            }
            catch(Exception ex)
            {
                AddToLogger(ex.Message);
                if (Connected)
                {
                    this.Connect();
                }
                retval = prop;
            }
            
            return retval;
        }
        public void AddToLogger(string msg)
        {
            _logger += msg + "\n";
            NotifyPropertyChanged("Logger");
        }
        public void ClearLogger()
        {
            _logger = "";
            NotifyPropertyChanged("Logger");
        }
    }
}
