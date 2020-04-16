using ClientSide;
using FlightSimulatorModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		public FlightSimulatorMapVM mapVM { get; internal set; }
		public FlightSimulatorLoginVM loginVM { get; internal set; }
		public FlightSimulatorDashboardVM dashboardVM { get; internal set; }
		public FlightSimulatorControlVM controlVM { get; internal set; }
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Client client = new Client();
			FlightSimulatorM model = new FlightSimulatorM(client);
			mapVM = new FlightSimulatorMapVM(model);
			loginVM = new FlightSimulatorLoginVM(model);
			dashboardVM = new FlightSimulatorDashboardVM(model);
			controlVM = new FlightSimulatorControlVM(model);
			// Create the startup window
			MainWindow wnd = new MainWindow();
			// Show the window
			//wnd.Show();
		}
	}
}
