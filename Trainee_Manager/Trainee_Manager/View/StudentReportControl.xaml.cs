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
    /// Interaction logic for StudentReportControl.xaml
    /// </summary>
    public partial class StudentReportControl : UserControl
    {
        private MainWindow mainWindow;

        public StudentReportControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void reportDropDown_DropDownClosed(object sender, EventArgs e)
        {
            if (reportDropDown.Text == "Studenten")
            {
                mainWindow.showStudentenReport();
            }
            else if (reportDropDown.Text == "Stages")
            {
                mainWindow.showStagesReport();
            }
            else if (reportDropDown.Text == "Docenten")
            {
                mainWindow.showInstructorsReport();
            }
            else if (reportDropDown.Text == "Bedrijven")
            {
                mainWindow.showCompaniesReport();
            }
        }

        private void printButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mainWindow.showStudentsReport();
        }
    }
}
