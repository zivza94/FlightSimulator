using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFlightSimulatorViewModel
    {

        // props
        Double VM_Heading { get; }
        Double VM_VerSpeed { get; }
        Double VM_GroundSpeed { get; }
        Double VM_AirSpeed { get; }
        Double VM_GpsAltitude { get; }
        Double VM_Roll { get; }
        Double VM_Pitch { get; }
        Double VM_AltAltitude { get; }
        Double VM_Latitude { get; }
        Double VM_Longitude { get; }

        Double VM_Throttle { get; set; }
        Double VM_Aileron { get; set; }



        void SetRudderAndElevator(double rudder, double elevator);
    }
}
