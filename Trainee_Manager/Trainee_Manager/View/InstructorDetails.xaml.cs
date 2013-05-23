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
    /// Interaction logic for InstructorDetails.xaml
    /// </summary>
    public partial class InstructorDetails : UserControl
    {
        //unique id of the instructor record in the database table "docenten"
        private int id;

        private static DataTable dataTable;

        public InstructorDetails(int id)
        {
            InitializeComponent();

            this.id = id;

            getData();
        }

        private void getData()
        {

            //Call the procedure to load the mysql data
            dataTable = DatabaseConnection.commandSelect("CALL procedure_docent_details(" + id + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                textbox_naam.Text = row["naam"].ToString();
                textbox_email.Text = row["email"].ToString();
                textbox_kamernummer.Text = row["kamernummer"].ToString();
                textbox_telefoonnummer.Text = row["telefoonnummer"].ToString();
                textbox_adres.Text = row["adres"].ToString();
                textbox_postcode.Text = row["postcode"].ToString();
                textbox_plaats.Text = row["plaats"].ToString();
                textbox_telefoonnummer_prive.Text = row["telefoonnummer_prive"].ToString();                
            }
        }
    }
}
