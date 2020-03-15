using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFlightSimulatorModel
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

        //for the client
        void Connect(string ip, int port);
        void Disconnect();
        void Start();
    }
}
