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
    public partial class InstructorTraineeReportControl : UserControl
    {

        private MainWindow mainWindow;
        private InstructorTraineeReport contentPage;

        public InstructorTraineeReportControl(MainWindow mainWindow, InstructorTraineeReport contentPage)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.contentPage = contentPage;
        }

        private void printButton_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
