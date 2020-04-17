using System.Windows;
using System.Windows.Controls;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        public Map()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).mapVM;
        }
    }
}
