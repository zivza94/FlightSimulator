using System.Windows;
using System.Windows.Controls;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for IndicatorsView.xaml
    /// </summary>
    public partial class IndicatorsView : UserControl
    {
        public IndicatorsView()
        {
            InitializeComponent();
            DataContext = DataContext = (Application.Current as App).dashboardVM;
        }
    }
}
