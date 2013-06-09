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
using Trainee_Manager.Model;

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for TraineeReport.xaml
    /// </summary>
    public partial class InstructorTraineeReport : UserControl
    {
        private MainWindow mainWindow;
        private static DataTable dataTable;
        private DataTable ids;

        private int docentId;

        public InstructorTraineeReport(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            //Deze code hebben we tijdelijk nodig, voor het geval we met onze FAKE DOCENT account inloggen
           // if (Session.ID.Equals(""))
           // {
               // Session.ID = "gbj.saris@avans.nl";
          //  }

            //Get the correct instructor
            getInstructor();

            //Show the instructors trainees
            getData(getLastPeriod());

            //Set the instructor id if needed on the preference page or my data page
            mainWindow.InstructorId = docentId;
        }

        private void getInstructor()
        {
            dataTable = DatabaseConnection.commandSelect("CALL procedure_docent_get_id('" + Session.ID + "');");

            foreach (DataRow row in dataTable.Rows)
            {
                docentId = Convert.ToInt32(row["id"]);
            }
        }

        private int getIdOfSelected()
        {
            int rowNumber = data.SelectedIndex;
            int indexDelete = Convert.ToInt32(ids.Rows[rowNumber][0]);

            return indexDelete;
        }

        private void removeFirstColumn()
        {
            ids = new DataTable("Idee");
            DataColumn c = new DataColumn("id");
            ids.Columns.Add(c);
            copyColumns(dataTable, ids, "id");
            dataTable.Columns.RemoveAt(0);
        }

        private void copyColumns(DataTable source, DataTable dest, params string[] columns)
        {
            foreach (DataRow sourcerow in source.Rows)
            {
                DataRow destRow = dest.NewRow();
                foreach (string colname in columns)
                {
                    destRow[colname] = sourcerow[colname];
                }
                dest.Rows.Add(destRow);
            }
        }

        //Call the procedure to load the mysql data
        public void getData(string periode)
        {
            dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_overzicht_vandocent(" + docentId + "," + Convert.ToInt32(periode) + ");");
            removeFirstColumn();

            //Set the datagrid context to the datatable
            data.DataContext = dataTable;
        }

        private string getLastPeriod()
        {
            string period = "";

            dataTable = DatabaseConnection.commandSelect("SELECT id FROM periodes ORDER BY id DESC LIMIT 1");
            foreach (DataRow row in dataTable.Rows)
            {
                period = row["id"].ToString();
            }

            return period;
        }

        private void data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("" + getIdOfSelected());
            mainWindow.showTraineeDetailsScreenViaInstructor(getIdOfSelected());
        }

        public void print()
        {
            ExportToExcel.exportDataTable(dataTable);
        }
    }
}
