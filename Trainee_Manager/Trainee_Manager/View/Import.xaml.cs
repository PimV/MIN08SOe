
using Microsoft.Win32;
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
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : UserControl
    {

        private string stagePath;
        private string urenPath;
        private string docentPath;
        private DataTable dataTable;


        public Import()
        {
            InitializeComponent();
            getData();
        }

        public void getData()
        {
            dataTable = DatabaseConnection.commandSelect("SELECT id, opleiding FROM opleidingen ORDER BY id ASC");
            foreach (DataRow row in dataTable.Rows)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                ComboBoxItem comboBoxItem2 = new ComboBoxItem();
                comboBoxItem.Content = row["opleiding"];
                comboBoxItem.Tag = row["id"];
                comboBoxItem2.Content = row["opleiding"];
                comboBoxItem2.Tag = row["id"];
                comboBox_Opleidingen_Copy.Items.Add(comboBoxItem);
                comboBox_Opleidingen.Items.Add(comboBoxItem2);
            }

            comboBox_Opleidingen.SelectedIndex = 0;
            comboBox_Opleidingen_Copy.SelectedIndex = 0;
        }

        private void import1_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (start1.ToString() != string.Empty 
                && end1.ToString() != string.Empty 
                && urenInzetBox.Text != string.Empty 
                && stageLijstBox.Text != string.Empty 
                && periodeBox.Text.Trim() != string.Empty 
                && periodeComboBox.SelectedIndex != -1
                && comboBox_Opleidingen.SelectedIndex != 0)
            {
                ImportController exc = new ImportController();
               
                exc.PeriodeNaam = periodeBox.Text;
                exc.Start1 = parseDate(start1.SelectedDate);
                exc.End1 = parseDate(end1.SelectedDate);
                exc.Periode = (int)periodeComboBox.SelectedItem;
                exc.OpleidingID = Int32.Parse(((ComboBoxItem)comboBox_Opleidingen.SelectedItem).Tag.ToString());
                
                if (exc.createPeriode() && exc.checkStageLijst(stagePath) && exc.checkUrenLijst(urenPath))
                {
                    MessageBox.Show("Import gelukt!");
                }
                else if (!exc.createPeriode())
                {
                    MessageBox.Show("Er bestaat al een periode met deze naam.");
                }
                else
                {
                    MessageBox.Show("Er is iets misgegaan met het importeren.");
                }
               // exc.checkStageLijst(stagePath);
              //  exc.checkUrenLijst(urenPath);
            }
            else
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("Ongeldige/geen data ingevoerd. Zorg ervoor dat alle gegevens ingevuld zijn en alle bestanden geselecteerd zijn.");
                //Mouse.OverrideCursor = Cursors.Arrow;
            }
            Mouse.OverrideCursor =  null;


        }
        
        private string parseDate(DateTime? dt)
        {
            string date = dt.ToString();
            string[] dateParts = date.Split(' ');
            date = dateParts[0];
            return date;
        }

        private void stagiaireBrowse(object sender, RoutedEventArgs e)
        {
            stagePath = getFileName();
            string[] stageParts = stagePath.Split('\\');
            stageLijstBox.Text = stageParts[stageParts.Length - 1];
        }

        private void urenInzetBrowse(object sender, RoutedEventArgs e)
        {
            urenPath = getFileName();
            string[] urenParts = urenPath.Split('\\');
            urenInzetBox.Text = urenParts[urenParts.Length - 1];
        }

        private string getFileName()
        {
            string fileName = "";
            try
            {
                OpenFileDialog o = new OpenFileDialog();
                Nullable<bool> result = o.ShowDialog();
                if (result == true)
                {
                    fileName = o.FileName;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Something went wrong: " + exc.ToString());
            }
            return fileName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            if (docentBox.Text != string.Empty)
            {
                ImportController exc = new ImportController();
                exc.OpleidingID = Int32.Parse(((ComboBoxItem)comboBox_Opleidingen_Copy.SelectedItem).Tag.ToString());
                exc.checkDocentenLijst(docentPath);
            }
            else
            {
                MessageBox.Show("Geen bestand geselecteerd.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            docentPath = getFileName();
            string[] docentParts = docentPath.Split('\\');
            docentBox.Text = docentParts[docentParts.Length - 1];
        }
    }
}
