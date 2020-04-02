using ClientSide;
using Contracts;
using FlightSimulatorModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestMAndVM
{
    class Program
    {
        //public static event PropertyChangedEventHandler PropertyChanged;

        static void Main(string[] args)
        {



            IFlightSimulatorModel model = new FlightSimulatorM(new Client());
            FlightSimulatorLoginVM loginVM = new FlightSimulatorLoginVM(model);
            FlightSimulatorControlVM controlVM = new FlightSimulatorControlVM(model);
            FlightSimulatorDashboardVM dashVM = new FlightSimulatorDashboardVM(model);
            FlightSimulatorMapVM mapVM = new FlightSimulatorMapVM(model);


            mapVM.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e) { Console.WriteLine(e.PropertyName + " has changed"); };
            dashVM.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e) { Console.WriteLine(e.PropertyName + " has changed"); };
            loginVM.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e) { Console.WriteLine(e.PropertyName + " has changed"); };
            controlVM.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e) { Console.WriteLine(e.PropertyName + " has changed"); };

            loginVM.StartConnection();
            double rudder = 0.05;
            double elevator = 0.01;
            double throttle = 0;
            double ailron = 0.003;
            double fact = 0.01;
            for (int i = 0; i < 100; i++)
            {
                rudder += fact;
                elevator += fact;
                ailron += fact;
                throttle += fact;
                controlVM.SetRudderAndElevator(rudder,elevator);
                controlVM.VM_Aileron = ailron;
                controlVM.VM_Throttle = throttle;
            }
            PrintData(controlVM,mapVM,dashVM,loginVM);
            
            
            loginVM.EndConnection();
            Console.ReadLine();
        }

        static void PrintData(FlightSimulatorControlVM controler, FlightSimulatorMapVM map, FlightSimulatorDashboardVM dashboard,FlightSimulatorLoginVM login)
        {
            //dashboard
            Console.WriteLine("*************VARS*************");
            Console.WriteLine("heading: " + dashboard.VM_Heading);
            Console.WriteLine("airSpeed: "+ dashboard.VM_AirSpeed);
            Console.WriteLine("altAltitude: "+ dashboard.VM_AltAltitude);
            Console.WriteLine("gpsAltitude: "+ dashboard.VM_GpsAltitude);
            Console.WriteLine("groundSpeed: "+ dashboard.VM_GroundSpeed);
            Console.WriteLine("pitch: "+ dashboard.VM_Pitch);
            Console.WriteLine("roll: "+ dashboard.VM_Roll);
            Console.WriteLine("verSpeed: "+ dashboard.VM_VerSpeed);
            //map
            Console.WriteLine("************MAP***************");
            Console.WriteLine("Latitude: " + map.VM_Latitude);
            Console.WriteLine("longtitude: "+ map.VM_Longitude);
            //setters
            Console.WriteLine("**********SETTERS************");
            Console.WriteLine("ailreon: "+ controler.VM_Aileron);
            Console.WriteLine("throrrle: "+ controler.VM_Throttle);

            Console.WriteLine("***********LOGGER************");
            Console.WriteLine("Logger: " +login.VM_Logger);
        }
    }
}
