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
    /// Interaction logic for TraineeDetails2.xaml
    /// </summary>
    public partial class TraineeDetailsEdit : UserControl
    {
        private int stageId;

        public TraineeDetailsEdit(int id)
        {
            InitializeComponent();
            stageId = id;
            MessageBox.Show("" + stageId);
            getCompanyData();
            getStudentData();
        }

        //Gets companies from databse and fills listbox_Company with them.
        private void getCompanyData()
        {
            DataTable tempTable = DatabaseConnection.commandSelect("SELECT * FROM bedrijven");
            listBox_Company.SelectedValuePath = "id";
            listBox_Company.DisplayMemberPath = "naam";
            listBox_Company.ItemsSource = tempTable.DefaultView;
        }

        //Gets students from databse and fills listbox_Student with them.
        private void getStudentData()
        {
            DataTable tempTable = DatabaseConnection.commandSelect("SELECT * FROM studenten");
            listBox_Student.SelectedValuePath = "id";
            listBox_Student.DisplayMemberPath = "achternaam";
            listBox_Student.ItemsSource = tempTable.DefaultView;
        }

        private void toggleStudentEditMode(object sender, RoutedEventArgs e)
        {
            if ((bool)CheckBox_Graduate.IsChecked)
            {
                radioButton_Student2.IsEnabled = true;
            }
            else
            {
                radioButton_Student1.IsChecked = true;
                radioButton_Student2.IsEnabled = false;
                textBox_studentName2.Text = "";
                textBox_studentNr2.Text = "";
            }
        }

        private void listBox_Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selection = (DataRowView)listBox_Company.SelectedItem;

            companyName.Text = selection.Row["naam"].ToString();
            companyPlaats.Text = selection.Row["straat"].ToString() + " " + selection.Row["nummer"].ToString();
            companyAdres.Text = selection.Row["plaats"].ToString();

            Console.WriteLine("This is the " + selection.Row["plaats"].ToString());
        }

        private void listBox_Student_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selection = (DataRowView)listBox_Student.SelectedItem;

            if ((bool)radioButton_Student1.IsChecked)
            {
                textbox_studentName.Text = selection.Row["roepnaam"].ToString() + " " + selection.Row["achternaam"].ToString();
                textbox_studentNr.Text = selection.Row["studentnr"].ToString();
            }
            if ((bool)radioButton_Student2.IsChecked)
            {
                if ((bool)CheckBox_Graduate.IsChecked)
                {
                    textBox_studentName2.Text = selection.Row["roepnaam"].ToString() + " " + selection.Row["achternaam"].ToString();
                    textBox_studentNr2.Text = selection.Row["studentnr"].ToString();
                }
            }
        }
    }
}
