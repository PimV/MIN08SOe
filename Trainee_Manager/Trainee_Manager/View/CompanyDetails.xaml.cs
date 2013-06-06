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
        private MainWindow mainWindow;

        private static DataTable dataTable;

        public CompanyDetails(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        //INDIEN MOGELIJK hieronder :base er neer zetten en initialize weg halen
        public CompanyDetails(MainWindow mainWindow, int id)
        {
            InitializeComponent();

            this.id = id;
            this.mainWindow = mainWindow;
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

        public void updateCompany()
        {
            if (id < 1)
            {
                //Call the procedure to insert the new instructor
                dataTable = DatabaseConnection.commandSelect("CALL procedure_bedrijf_add('" + textbox_naam.Text + "','" + textbox_branche.Text + "','" + textbox_plaats.Text + "','" + textbox_straat.Text + "','" + textbox_straatnummer.Text + "','" + textbox_straattoevoeging.Text + "','" + textbox_land.Text + "','" + textbox_postcode.Text + "','" + textbox_telefoonnummer.Text + "','" + textbox_website.Text + "','" + textbox_opmerking.Text + "');");

                //Show message that the new instructor is added
                MessageBoxResult messageBox = MessageBox.Show("Het bedrijf is toegevoegd aan het systeem. U word nu terug gestuurd naar de overzicht pagina.", "Opgeslagen", MessageBoxButton.OK, MessageBoxImage.Information);

                mainWindow.showCompaniesReport();
            }
            else
            {
                MessageBoxResult messageBox = MessageBox.Show("Weet u zeker dat u de bedrijfs gegevens wilt aanpassen?", "Aanpassen?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBox == MessageBoxResult.Yes)
                {
                    //Call the procedure to update the mysql data
                    dataTable = DatabaseConnection.commandSelect("CALL procedure_bedrijf_details_update(" + id + ",'" + textbox_naam.Text + "','" + textbox_branche.Text + "','" + textbox_straat.Text + "','" + textbox_straatnummer.Text + "','" + textbox_straattoevoeging.Text + "','" + textbox_postcode.Text + "','" + textbox_plaats.Text + "','" + textbox_land.Text + "','" + textbox_telefoonnummer.Text + "','" + textbox_website.Text + "','" + textbox_opmerking.Text + "');");
                }
            }

        }

        private void nummer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }
    }
}
