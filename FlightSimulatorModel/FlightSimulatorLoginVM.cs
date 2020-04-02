using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorModel
{
    public class FlightSimulatorLoginVM
    {
        readonly private IFlightSimulatorModel _model;

        public void EndConnection()
        {
            _model.Disconnect();
        }
        public void StartConnection()
        {
            _model.Connect("localhost", 5402);
            _model.Start();
        }

    }
}
