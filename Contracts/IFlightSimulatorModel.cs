using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFlightSimulatorModel : INotifyPropertyChanged
    {
        // props
        Double Heading{ get; set; }
        Double VerSpeed{ get; set; }
        Double GroundSpeed { get; set; }
        Double AirSpeed { get; set; }
        Double GpsAltitude { get; set; }
        Double Roll { get; set; }
        Double Pitch { get; set; }
        Double AltAltitude { get; set; }
        Double Latitude { get; set; }
        Double Longitude { get; set; }
        string Logger { get; }
        bool Connected { get; set; }

        //for the client
        void Connect(string ip, string port);
        void Disconnect();
        void Start();

        void SetRudderAndElevator(double rudder, double elevator);
        void SetAileron(double aileron);
        void SetThrottle(double throttle);
    }
}
