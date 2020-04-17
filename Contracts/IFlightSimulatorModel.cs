using System;
using System.ComponentModel;


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
        string Ip { get; set; }
        string Port { get; set; }

        //for the client
        void Connect();
        void Disconnect();
        void Start();

        void SetRudderAndElevator(double rudder, double elevator);
        void SetAileron(double aileron);
        void SetThrottle(double throttle);
    }
}
