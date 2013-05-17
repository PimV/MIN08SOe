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
    /// Interaction logic for CompanyReportControl.xaml
    /// </summary>
    public partial class CompanyReportControl : UserControl
    {
        
        private MainWindow mainWindow;

        public CompanyReportControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void printButton_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void reportDropDown_DropDownClosed(object sender, EventArgs e)
        {
            if (reportDropDown.Text == "Stages")
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
    }
}
