using Contracts;
using FlightSimulatorModel;
using System;
using Telnet;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ITelnetClient client = new TelnetClient();
            IFlightSimulatorModel fm = new MyFlightSimulatorModel(client);
            fm.Connect("localhost", 5402);
            fm.Start();
            Console.ReadLine();
            fm.Disconnect();

        }
    }
}
