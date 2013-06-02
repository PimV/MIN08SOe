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
        public TraineeDetailsEdit()
        {
            InitializeComponent();
            getCompanyData();
        }

        //Gets companies from databse and fills listbox_Company with them.
        private void getCompanyData()
        {
            DataTable tempTable = DatabaseConnection.commandSelect("SELECT * FROM bedrijven");
            listBox_Company.SelectedValuePath = "id";
            listBox_Company.DisplayMemberPath = "naam";
            listBox_Company.ItemsSource = tempTable.DefaultView;
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
                textBox_Student2.Text = "";
                textBox_Student2Number.Text = "";
            }
        }
    }
}
