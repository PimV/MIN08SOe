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
        private MainWindow mainWindow;

        //unique id of the instructor record in the database table "docenten"
        private int id;

        private static DataTable dataTable;


        public InstructorDetails(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            textbox_naam.IsEnabled = true;
        }

        public InstructorDetails(MainWindow mainWindow, int id)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

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

        public void updateInstructor()
        {
            if (id < 1)
            {
                //Call the procedure to insert the new instructor
                dataTable = DatabaseConnection.commandSelect("CALL procedure_docent_details_add('" + textbox_naam.Text + "','" + textbox_email.Text + "','" + textbox_kamernummer.Text + "','" + textbox_telefoonnummer.Text + "','" + textbox_adres.Text + "','" + textbox_postcode.Text + "','" + textbox_plaats.Text + "','" + textbox_telefoonnummer_prive.Text + "');");
                
                //Show message that the new instructor is added
                MessageBoxResult result = MessageBox.Show("De docent is toegevoegd aan het systeem. U word nu terug gestuurd naar de overzicht pagina.", "Opgeslagen", MessageBoxButton.OK, MessageBoxImage.Information);

                mainWindow.showInstructorsReport();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Weet u zeker dat u de docent gegevens wilt aanpassen?", "Aanpassen?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    //Call the procedure to update the mysql data
                    dataTable = DatabaseConnection.commandSelect("CALL procedure_docent_details_update(" + id + ",'" + textbox_kamernummer.Text + "','" + textbox_telefoonnummer.Text + "','" + textbox_adres.Text + "','" + textbox_postcode.Text + "','" + textbox_plaats.Text + "','" + textbox_telefoonnummer_prive.Text + "');");
                }
            }

        }
    }
}
