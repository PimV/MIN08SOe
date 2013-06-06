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
        private MainWindow mainWindow;

        private static DataTable dataTable;

        public StudentDetails(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            setAddMode();
        }

        public StudentDetails(MainWindow mainWindow, int id)
        {
            InitializeComponent();

            this.id = id;
            this.mainWindow = mainWindow;
            getData();
        }

        private void setAddMode()
        {
            label_Period.Visibility = Visibility.Visible;
            combobox_Period.Visibility = Visibility.Visible;

            fillComboboxPeriod();

            combobox_Period.SelectedIndex = 0;

            textbox_studentnummer.IsEnabled = true;
            textbox_voorletters.IsEnabled = true;
            textbox_roepnaam.IsEnabled = true;
            textbox_achternaam.IsEnabled = true;
        }

        private void fillComboboxPeriod()
        {
            dataTable = DatabaseConnection.commandSelect("SELECT id, periode FROM periodes ORDER BY id DESC LIMIT 3");
            foreach (DataRow row in dataTable.Rows)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = row["periode"];
                comboBoxItem.Tag = row["id"];
                combobox_Period.Items.Add(comboBoxItem);
            }
        }

        private void getData()
        {

            //Call the procedure to load the mysql data
            dataTable = DatabaseConnection.commandSelect("CALL procedure_student_details(" + id + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                //HIERONDER VERDER AF MAKEN
                textbox_studentnummer.Text = row["studentnr"].ToString();
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

        public void updateStudent()
        {
            if (id < 1)
            {
                string periode = ((ComboBoxItem)combobox_Period.SelectedItem).Content.ToString();

                dataTable = DatabaseConnection.commandSelect("CALL procedure_student_details_import(" + Int32.Parse(textbox_studentnummer.Text) + ",'" + textbox_achternaam.Text + "','" + textbox_voorletters.Text + "','" + textbox_roepnaam.Text + "','" + textbox_email.Text + "','" + textbox_straat.Text + "','" + Convert.ToInt32(textbox_huisnummer.Text) + "','" + textbox_huistoevoeging.Text + "','" + textbox_postcode.Text + "','" + textbox_plaats.Text + "','" + textbox_telefoonnummer.Text + "','" + textbox_opmerking.Text + "','" + periode + "');");

                //Show message that the new student is added
                MessageBoxResult messageBox = MessageBox.Show("De student is toegevoegd aan het systeem. U word nu terug gestuurd naar de overzicht pagina.", "Opgeslagen", MessageBoxButton.OK, MessageBoxImage.Information);

                mainWindow.showStudentsReport();
            }
            else
            {
                MessageBoxResult messageBox = MessageBox.Show("Weet u zeker dat u de student gegevens wilt aanpassen?", "Aanpassen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBox == MessageBoxResult.Yes)
                {
                    //Call the procedure to update the mysql data
                    dataTable = DatabaseConnection.commandSelect("CALL procedure_student_details_update(" + id + ",'" + textbox_email.Text + "','" + textbox_straat.Text + "','" + textbox_huisnummer.Text + "','" + textbox_huistoevoeging.Text + "','" + textbox_postcode.Text + "','" + textbox_plaats.Text + "','" + textbox_telefoonnummer.Text + "','" + textbox_opmerking.Text + "');");
                }
            }

        }

        private void huisnummer_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
