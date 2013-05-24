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
    /// Interaction logic for CompanyDetails.xaml
    /// </summary>
    public partial class CompanyDetails : UserControl
    {
        //unique id of the companhy record in the database table "bedrijven"
        private int id;

        private static DataTable dataTable;

        public CompanyDetails(int id)
        {
            InitializeComponent();

            this.id = id;
            getData();
        }

        private void getData()
        {

            //Call the procedure to load the mysql data
            dataTable = DatabaseConnection.commandSelect("CALL procedure_bedrijf_details(" + id + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                textbox_naam.Text = row["naam"].ToString();
                textbox_branche.Text = row["branche"].ToString();
                textbox_straat.Text = row["straat"].ToString();
                textbox_straatnummer.Text = row["nummer"].ToString();
                textbox_straattoevoeging.Text = row["toevoeging"].ToString();
                textbox_postcode.Text = row["postcode"].ToString();
                textbox_plaats.Text = row["plaats"].ToString();
                textbox_land.Text = row["land"].ToString();
                textbox_telefoonnummer.Text = row["telefoonnummer"].ToString();
                textbox_website.Text = row["website"].ToString();
                textbox_opmerking.Text = row["opmerking"].ToString();
            }
        }
    }
}
