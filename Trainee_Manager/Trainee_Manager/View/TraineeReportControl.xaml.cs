using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for TraineeReportControl.xaml
    /// </summary>
    public partial class TraineeReportControl : UserControl
    {

        private MainWindow mainWindow;
        private TraineeReport traineeReport;
        
        public TraineeReportControl(MainWindow mainWindow, TraineeReport traineeReport)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.traineeReport = traineeReport;
        }

        private void printButton_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonVerwijder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
