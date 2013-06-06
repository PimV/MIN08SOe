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
    /// Interaction logic for studentReport.xaml
    /// </summary>
    public partial class StudentReport : UserControl
    {
        private MainWindow mainWindow;
        private static DataTable dataTable;
        private DataTable ids;

        public StudentReport(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            getData();

            removeFirstColumn();

            //Set the datagrid context to the datatable
            data.DataContext = dataTable;
        }

        public void deleteStudent()
        {

            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u de student  wilt verwijderen?", "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int indexDelete = getIdOfSelected();

                dataTable = DatabaseConnection.commandSelect("CALL procedure_student_details_delete(" + indexDelete + ");");
                foreach (DataRow row in dataTable.Rows)
                {
                    MessageBoxResult result2 = MessageBox.Show(row["foutmelding"].ToString(), "Verwijderen mislukt", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                mainWindow.showStudentsReport();
            }
        }

        //Call the procedure to load the mysql data
        private void getData()
        {
            dataTable = DatabaseConnection.commandSelect("CALL procedure_student_overzicht();");
        }

        private void data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int rowNumber = getIdOfSelected();

            if (rowNumber != -1)
            {
                mainWindow.showStudentDetails(rowNumber);
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
    }
}
