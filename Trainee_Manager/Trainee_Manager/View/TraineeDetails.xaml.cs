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

            getData();
        }

        private void getData()
        {

            //Call the procedure to load the mysql data
            dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_details(" + id + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                label_studentnummer.Content = row["studentnummer"].ToString();
                label_studentnaam.Content = row["student"].ToString();
                label_studentemail.Content = row["email"].ToString();
                label_studentnummer2.Content = row["studentnummer2"].ToString();
                label_studentnaam2.Content = row["student2"].ToString();
                label_studentemail2.Content = row["email2"].ToString();
                label_begeleider.Content = row["begeleider"].ToString();
                label_lezer.Content = row["lezer"].ToString();
                textblock_onderwerpen.Text = row["onderwerpen"].ToString();
                label_bedrijf.Content = row["bedrijf"].ToString();
                label_bedrijfsplaats.Content = row["locatie"].ToString();
                label_bedrijfsadres.Content = row["adres"].ToString();
                label_bedrijfssite.Content = row["website"].ToString();
                textblock_opmerking.Text = row["opmerking"].ToString();
                label_bedrijfsbegeleider.Content = row["bedrijfsbegeleider"].ToString();
                label_bedrijfsbegeleideremail.Content = row["bedrijfsbegeleider_email"].ToString();
                label_bedrijfsbegeleidertel.Content = row["bedrijfsbegeleider_tel"].ToString();
                textblock_opdracht.Text = row["opdracht"].ToString();

                //ja of nee ipv true or false
                if (row["afstudeerstage"].ToString().Equals("True"))
                {
                    label_afstudeerstage.Content = "Ja";
                }
                else
                {
                    label_afstudeerstage.Content = "Nee";
                }

                //ja of nee ipv true or false
                if (row["toestemming"].ToString().Equals("True"))
                {
                    label_toestemming.Content = "Ja";
                }
                else
                {
                    label_toestemming.Content = "Nee";
                }

                //ja of nee ipv true or false
                if (row["goedkeuring"].ToString().Equals("True"))
                {
                    label_goedkeuring.Content = "Ja";
                }
                else
                {
                    label_goedkeuring.Content = "Nee";
                }
            }
        }
    }
}
