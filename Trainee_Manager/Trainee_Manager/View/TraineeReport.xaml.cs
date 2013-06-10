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
    /// Interaction logic for TraineeReport.xaml
    /// </summary>
    public partial class TraineeReport : UserControl
    {
        private MainWindow mainWindow;
        private static DataTable dataTable;

        public TraineeReport(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            
            getData("0", getLastPeriod(), null);
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

        //Call the procedure to load the mysql data
        public void getData(string begeleider, string periode, string zoek)
        {
            zoek = "%" + zoek + "%";
            dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_overzicht(" + Convert.ToInt32(begeleider) + "," + Convert.ToInt32(periode) + ",'" + zoek + "');");
            //Set the datagrid context to the datatable
            data.DataContext = dataTable;
        }

        private void data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = Convert.ToInt32((data.SelectedCells[0].Item as DataRowView).Row[0].ToString());

            mainWindow.TraineeId = id;
            mainWindow.showTraineeDetailsScreen();
        }

        public void print()
        {
            ExportToExcel.exportDataTable(dataTable);
        }

        private void data_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID" || e.PropertyName == string.Empty)
            {
                e.Cancel = true;
            }
        }

    }
}
