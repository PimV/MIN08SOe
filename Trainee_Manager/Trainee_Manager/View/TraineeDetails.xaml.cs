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
            dataTable = DatabaseConnection.commandSelect("CALL procedure_trainee_details();");

            foreach (DataRow row in dataTable.Rows)
            {
                label_studentnummer.Content = row["studentnummer"].ToString();
                label_studentnaam.Content = row["student"].ToString();
                label_studentemail.Content = row["email"].ToString();
                label_begeleider.Content = row["begeleider"].ToString();
                label_lezer.Content = row["lezer"].ToString();
                label_afstudeerstage.Content = row["afstudeerstage"].ToString();
                textblock_onderwerpen.Text = row["onderwerpen"].ToString();
                label_bedrijf.Content = row["bedrijf"].ToString();
                label_bedrijfsplaats.Content = row["locatie"].ToString();
                label_bedrijfsadres.Content = row["adres"].ToString();
                label_bedrijfssite.Content = row["website"].ToString();
                label_toestemming.Content = row["toestemming"].ToString();
                label_goedkeuring.Content = row["goedkeuring"].ToString();
                textblock_opmerking.Text = row["opmerking"].ToString();
                label_bedrijfsbegeleider.Content = row["bedrijfsbegeleider"].ToString();
                label_bedrijfsbegeleideremail.Content = row["bedrijfsbegeleider_email"].ToString();
                label_bedrijfsbegeleidertel.Content = row["bedrijfsbegeleider_tel"].ToString();
                textblock_opdracht.Text = row["opdracht"].ToString();
            }
        }
    }
}
