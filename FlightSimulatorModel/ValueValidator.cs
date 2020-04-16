using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorModel
{
    public class ValueValidator
    {
        Dictionary<string, KeyValuePair<double, double>> _ValuesBoundry;
        public ValueValidator()
        {
            _ValuesBoundry = new Dictionary<string, KeyValuePair<double, double>>();
            /*
            //dashBoard
            _ValuesBoundry.Add("Heading", new KeyValuePair<double, double>(0,360));
            _ValuesBoundry.Add("VerSpeed", new KeyValuePair<double, double>(-5000,721));
            _ValuesBoundry.Add("GroundSpeed", new KeyValuePair<double, double>(-50,302));
            _ValuesBoundry.Add("AirSpeed", new KeyValuePair<double, double>(0,228));
            _ValuesBoundry.Add("GpsAltitude", new KeyValuePair<double, double>(0,13500));
            //_ValuesBoundry.Add("Roll", new KeyValuePair<double, double>());
            //_ValuesBoundry.Add("Pitch", new KeyValuePair<double, double>(10, 50));
            _ValuesBoundry.Add("AltAltitude", new KeyValuePair<double, double>(0,13500));
            */

            //controlers
            _ValuesBoundry.Add("Rudder", new KeyValuePair<double, double>(-1,1));
            _ValuesBoundry.Add("Elevator", new KeyValuePair<double, double>(-1,1));
            _ValuesBoundry.Add("Aileron", new KeyValuePair<double, double>(-1,1));
            _ValuesBoundry.Add("Throttle", new KeyValuePair<double, double>(0,1));

            //map
            _ValuesBoundry.Add("Latitude", new KeyValuePair<double, double>(-90, 90));
            _ValuesBoundry.Add("Longitude", new KeyValuePair<double, double>(-180, 180));
        }

        public double GetValue(string name,double value)
        {
            double retval = value;
            if (_ValuesBoundry.ContainsKey(name))
            {
                double min = _ValuesBoundry[name].Key;
                double max = _ValuesBoundry[name].Value;
                if (value > max)
                {
                    retval = max;
                }
                else if (value < min)
                {
                    retval = min;
                }
            }    
            return retval;
        }
    }
}
