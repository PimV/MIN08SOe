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
    /// Interaction logic for StudentDetails.xaml
    /// </summary>
    public partial class StudentDetails : UserControl
    {
        private int id;

        private static DataTable dataTable;

        public StudentDetails(int id)
        {
            InitializeComponent();

            this.id = id;
            getData();
        }

        private void getData()
        {

            //Call the procedure to load the mysql data
            dataTable = DatabaseConnection.commandSelect("CALL procedure_student_details(" + id + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                //HIERONDER VERDER AF MAKEN
                textbox_studentnummer.Text = row["studentnr"].ToString();
                textbox_studentcode.Text = row["studentcode"].ToString();
                textbox_achternaam.Text = row["achternaam"].ToString();
                textbox_voorletters.Text = row["voorvoegsels"].ToString();
                textbox_roepnaam.Text = row["roepnaam"].ToString();
                textbox_email.Text = row["email"].ToString();
                textbox_straat.Text = row["straatnaam"].ToString();
                textbox_huisnummer.Text = row["nummer"].ToString();
                textbox_huistoevoeging.Text = row["toevoeging"].ToString();
                textbox_postcode.Text = row["postcode"].ToString();
                textbox_plaats.Text = row["plaats"].ToString();
                textbox_telefoonnummer.Text = row["telefoonnummer"].ToString();
                textbox_opmerking.Text = row["opmerking"].ToString();
            }
        }
    }
}
