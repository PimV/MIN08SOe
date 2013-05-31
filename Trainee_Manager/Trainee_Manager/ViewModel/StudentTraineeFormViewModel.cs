using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trainee_Manager.Controller;

namespace Trainee_Manager.ViewModel
{
    class StudentTraineeFormViewModel
    {

        private int id;
        private static DataTable dataTable;
        private MainWindow mainWindow;

        public StudentTraineeFormViewModel(MainWindow mainWindow, int id)
        {
            this.id = id;
            this.mainWindow = mainWindow;
            checkData();
        }

        private void checkData()
        {

            //Check to see if the user has any trainee records. 
            dataTable = DatabaseConnection.commandSelect("SELECT COUNT(*) AS aantal "+
                                                         "FROM "+
                                                         "stages "+
                                                         "LEFT JOIN studenten as stu ON stages.student_id = stu.id "+
                                                         "LEFT JOIN studenten as stu2 ON stages.student_id_2 = stu2.id "+
                                                         "WHERE " +
                                                         "stu.studentnr = " + id + " OR stu2.studentnr = " + id);
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["aantal"].ToString() == "0")
                {
                    MessageBox.Show("Er staan geen stages geregistreerd op uw naam.");
                    mainWindow.showLoginScreen();
                }
            }
        }

    }
}
