using System;
using System.Collections.Generic;
using System.Data;
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
using Trainee_Manager.Controller;

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : UserControl
    {
        private int docentId;
        private static DataTable dataTable;

        public Preferences(int id)
        {
            InitializeComponent();

            docentId = id;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabSubjects.IsSelected)
            {
                tabSubjectsOpend();
            }
            if (tabCompanys.IsSelected)
            {
                tabCompanysOpend();
            }
            if (tabTrainees.IsSelected)
            {
                tabTraineesOpend();
            }
        }

        private void tabSubjectsOpend()
        {
            //Call the procedure to load the subjects not already linked to the instructor
            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_kenmerken_not_docent(" + docentId + ");");
            //listboxAllSubjects.DataContext = dataTable;
            foreach (DataRow row in dataTable.Rows)
            {
                listboxAllSubjects.Items.Add(row["naam"].ToString());
                //textbox_naam.Text = row["naam"].ToString();
            }

            //Call the procedure to load the subjects linked to the instructor
            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_kenmerken_docent(" + docentId + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                listboxChoosenSubjects.Items.Add(row["naam"].ToString());
                //textbox_naam.Text = row["naam"].ToString();
            }
        }

        private void tabCompanysOpend()
        {
        }

        private void tabTraineesOpend()
        {
        }
    }
}
