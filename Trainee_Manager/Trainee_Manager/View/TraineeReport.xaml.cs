﻿using System;
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
        private DataTable ids;

        public TraineeReport(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            //getData();

            //removeFirstColumn();

            //Set the datagrid context to the datatable
            //data.DataContext = dataTable;
        }

        //Call the procedure to load the mysql data
        private void getData()
        {
            dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_overzicht();");
        }

        private void data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int rowNumber = getIdOfSelected();

            if (rowNumber != -1)
            {
                mainWindow.TraineeId = rowNumber;
                mainWindow.showTraineeDetailsScreen();
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
