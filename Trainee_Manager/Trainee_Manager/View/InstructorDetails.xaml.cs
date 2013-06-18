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
using Trainee_Manager.Model;

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for InstructorDetails.xaml
    /// </summary>
    public partial class InstructorDetails : UserControl
    {
        private MainWindow mainWindow;

        //unique id of the instructor record in the database table "docenten"
        private int id = -1;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private static DataTable dataTable;


        public InstructorDetails(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            id = -1;
            setViewForNewInstructor();
        }

        public InstructorDetails(MainWindow mainWindow, int id)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            this.id = id;
            getData();
        }


        private void setViewForNewInstructor()
        {
            textbox_naam.IsEnabled = true;
            textbox_email.IsEnabled = true;

            listBoxCompanys.Visibility = Visibility.Collapsed;
            listBoxSubjects.Visibility = Visibility.Collapsed;
            listBoxTrainees.Visibility = Visibility.Collapsed;

            label_Voorkeurbedrijven.Visibility = Visibility.Collapsed;
            label_Voorkeuronderwerpen.Visibility = Visibility.Collapsed;
            label_Voorkeurstages.Visibility = Visibility.Collapsed;
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

            //Call the procedure to load the subjects linked to the instructor
            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_kenmerken_docent(" + id + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                listBoxSubjects.Items.Add(row["naam"].ToString());
            }

            //Call the procedure to load the companys linked to the instructor
            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_bedrijf_docent(" + id + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                listBoxCompanys.Items.Add(row["naam"].ToString());
            }

            //Call the procedure to load the companys linked to the instructor
            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_stage_docent(" + id + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                listBoxTrainees.Items.Add(row["naam"].ToString());
            }
        }

        public void updateInstructor()
        {
            if (textbox_email.Text.Trim() != string.Empty)
            {
                MessageBoxResult result = MessageBox.Show("Weet u zeker dat u de docent gegevens wilt aanpassen?", "Aanpassen?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (Object o in this.grid.Children)
                    {
                        if (o is TextBox)
                        {
                            TextBox box = (TextBox)o;
                            if (box.Text == string.Empty)
                            {
                                (o as TextBox).Text = "";
                            }
                        }
                    }

                    //Call the procedure to update the mysql data
                    dataTable = DatabaseConnection.commandSelect("CALL procedure_docent_details_update(" + id + ",'" + textbox_kamernummer.Text + "','" + textbox_telefoonnummer.Text + "','" + textbox_adres.Text + "','" + textbox_postcode.Text + "','" + textbox_plaats.Text + "','" + textbox_telefoonnummer_prive.Text + "');");

                    if (Session.Function != "Docent")
                    {
                        mainWindow.showInstructorsReport();                        
                    }
                }
            }
            else
            {
                MessageBox.Show("Geen email ingevoerd. Deze is verplicht.");
            }

        }

        public void addInstructor()
        {
            if (textbox_email.Text.Trim() != string.Empty)
            {

                Boolean emailAppears = true;
                DataTable test = DatabaseConnection.commandSelect("SELECT COUNT(*) FROM docenten WHERE email = '" + textbox_email.Text + "'");
                if (Int32.Parse(test.Rows[0][0].ToString()) < 1)
                {
                    emailAppears = false;
                }
                if (!emailAppears)
                {
                    if (textbox_email.Text.Contains("@"))
                    {
                        //Call the procedure to insert the new instructor
                        dataTable = DatabaseConnection.commandSelect("CALL procedure_docent_details_add('" + textbox_naam.Text + "','" + textbox_email.Text + "','" + textbox_kamernummer.Text + "','" + textbox_telefoonnummer.Text + "','" + textbox_adres.Text + "','" + textbox_postcode.Text + "','" + textbox_plaats.Text + "','" + textbox_telefoonnummer_prive.Text + "');");

                        //Show message that the new instructor is added
                        MessageBoxResult result = MessageBox.Show("De docent is toegevoegd aan het systeem. U word nu terug gestuurd naar de overzicht pagina.", "Opgeslagen", MessageBoxButton.OK, MessageBoxImage.Information);

                        mainWindow.showInstructorsReport();
                    }
                    else
                    {
                        MessageBox.Show("Email adres ongeldig (geen @).");
                    }
                }
                else
                {
                    MessageBox.Show("Email adres bestaat al.");
                }

            }
            else
            {
                MessageBox.Show("Geen email ingevoerd. Deze is verplicht.");
            }
        }
    }
}
