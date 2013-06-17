using Microsoft.Win32;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

            getData(null);
        }

        public void deleteStudent()
        {
            if (data.SelectedCells.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Weet u zeker dat u de student  wilt verwijderen?", "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int indexDelete = Convert.ToInt32((data.SelectedCells[0].Item as DataRowView).Row[0].ToString());

                    dataTable = DatabaseConnection.commandSelect("CALL procedure_student_details_delete(" + indexDelete + ");");
                    foreach (DataRow row in dataTable.Rows)
                    {
                        MessageBoxResult result2 = MessageBox.Show(row["foutmelding"].ToString(), "Verwijderen mislukt", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    mainWindow.showStudentsReport();
                }
            }
        }

        //Call the procedure to load the mysql data
        public void getData(string zoek)
        {
            zoek = "%" + zoek + "%";

            dataTable = DatabaseConnection.commandSelect("CALL procedure_student_overzicht('" + zoek + "');");

            //Set the datagrid context to the datatable
            data.DataContext = dataTable;
        }

        private void data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (data.SelectedCells.Count > 0)
            {
                int id = Convert.ToInt32((data.SelectedCells[0].Item as DataRowView).Row[0].ToString());
                mainWindow.showStudentDetails(id);
            }
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
