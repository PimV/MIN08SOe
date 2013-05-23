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
    /// Interaction logic for TraineeDetails.xaml
    /// </summary>
    public partial class TraineeDetails : UserControl
    {
        private int id;

        private static DataTable dataTable;


        public TraineeDetails(int id)
        {
            InitializeComponent();

            //Id represents the id in the stage table
            this.id = id;

            //Call the procedure to load the mysql data
            dataTable = DatabaseConnection.commandSelect("SELECT studenten.roepnaam AS naam, studenten.email, studenten.studentnr AS nr FROM stages INNER JOIN studenten ON stages.student_id = studenten.id WHERE stages.id = '" + id + "'");

            foreach (DataRow row in dataTable.Rows)
            {
                label_studentnaam.Content = row["naam"].ToString();
                label_studentemail.Content = row["email"].ToString();
                label_studentnummer.Content = row["nr"].ToString();
            }
        }
    }
}
