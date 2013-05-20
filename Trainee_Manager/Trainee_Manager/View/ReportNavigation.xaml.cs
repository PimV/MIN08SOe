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
    /// Interaction logic for ReportNavigation.xaml
    /// </summary>
    public partial class ReportNavigation : UserControl
    {

        private MainWindow mainWindow;

        public int SelectedItem
        {
            get { return reportDropDown.SelectedIndex; }
            set { reportDropDown.SelectedIndex = value; }
        }
        
        public ReportNavigation()
        {
            InitializeComponent();
        }

        public void initialize(MainWindow mainWindow, String defaultSelection)
        {
            this.mainWindow = mainWindow;
            reportDropDown.Text = defaultSelection;
        }

        private void reportDropDown_DropDownClosed(object sender, EventArgs e)
        {
            try
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
                else if (reportDropDown.Text == "Studenten")
                {
                    mainWindow.showStudentsReport();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("ReportNavigation not correctly initialized. Use the initialize(MainWindow mainWindow, String defaultSelection) method!", exception);
            }
            
        }
    }
}
