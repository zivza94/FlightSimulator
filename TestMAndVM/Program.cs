using ClientSide;
using FlightSimulatorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestMAndVM
{
    class Program
    {
        /*static void Main(string[] args)
        {
            FlightSimulatorControlVM vm = new FlightSimulatorControlVM(new FlightSimulatorModel.FlightSimulatorModel(new Client()));
            vm.StartConnection();
            double rudder = 0.05;
            double elevator = 0.01;
            double throttle = 0;
            double ailron = 0.003;
            double fact = 0.01;
            for (int i = 0; i < 100000; i++)
            {
                rudder += fact;
                elevator += fact;
                ailron += fact;
                throttle += fact;
                vm.SetRudderAndElevator(rudder,elevator);
                vm.VM_Aileron = ailron;
                vm.VM_Throttle = throttle;
            }
            PrintData(vm);
            
            vm.EndConnection();
            Console.ReadLine();
        }

        static void PrintData(FlightSimulatorControlVM controler, FlightSimulatorMapVM map, FlightSimulatorDashboardVM dashboard)
        {
            //dashboard
            Console.WriteLine("*************VARS*************");
            Console.WriteLine("heading: " + vm.VM_Heading);
            Console.WriteLine("airSpeed: "+vm.VM_AirSpeed);
            Console.WriteLine("altAltitude: "+vm.VM_AltAltitude);
            Console.WriteLine("gpsAltitude: "+vm.VM_GpsAltitude);
            Console.WriteLine("groundSpeed: "+vm.VM_GroundSpeed);
            Console.WriteLine("pitch: "+vm.VM_Pitch);
            Console.WriteLine("roll: "+vm.VM_Roll);
            Console.WriteLine("verSpeed: "+vm.VM_VerSpeed);
            //map
            Console.WriteLine("************MAP***************");
            Console.WriteLine("Latitude: " +vm.VM_Latitude);
            Console.WriteLine("longtitude: "+vm.VM_Longitude);
            //setters
            Console.WriteLine("**********SETTERS************");
            Console.WriteLine("ailreon: "+vm.VM_Aileron);
            Console.WriteLine("throrrle: "+vm.VM_Throttle);
        }*/
    }
}
