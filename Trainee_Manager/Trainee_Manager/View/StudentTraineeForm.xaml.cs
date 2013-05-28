using System;
using System.Collections.Generic;
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

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for StudentTraineeForm.xaml
    /// </summary>
    public partial class StudentTraineeForm : UserControl
    {

        private int id;

        public StudentTraineeForm(int id)
        {
            InitializeComponent();
            this.id = id;
            Console.WriteLine("IntStudentTraineeForm: " + this.id);

        }

        private void otherCheckBox_Click(object sender, RoutedEventArgs e)
        {
            toggleCompanyEditMode();
        }

        private void toggleStudentEditMode(object sender, RoutedEventArgs e)
        {
            toggleStudentEditMode();
        }

        //Toggle the editing mode of the company fields.
        private void toggleCompanyEditMode()
        {
            //Turn on edit-mode.
            if ((bool)otherCheckBox.IsChecked)
            {
                /*//Change color of the fields to white
                companyName.Background = Brushes.White;
                companyBranche.Background = Brushes.White;
                companyCity.Background = Brushes.White;
                companyStreet.Background = Brushes.White;
                companyHouseNumber.Background = Brushes.White;
                companyHouseNumberAdd.Background = Brushes.White;
                companyCountry.Background = Brushes.White;
                companyPostalCode.Background = Brushes.White;
                companyPhoneNumber.Background = Brushes.White;
                companyMail.Background = Brushes.White;
                companyWebsite.Background = Brushes.White;*/

                //Enable the company fields.
                companyName.IsEnabled = true;
                companyBranche.IsEnabled = true;
                companyCity.IsEnabled = true;
                companyStreet.IsEnabled = true;
                companyHouseNumber.IsEnabled = true;
                companyHouseNumberAdd.IsEnabled = true;
                companyCountry.IsEnabled = true;
                companyPostalCode.IsEnabled = true;
                companyPhoneNumber.IsEnabled = true;
                companyMail.IsEnabled = true;
                companyWebsite.IsEnabled = true;
            }
            //turn off edit-mode 
            else
            {
                /*BrushConverter bc = new BrushConverter();
                Brush GreyBrush = (Brush)bc.ConvertFrom("#FFEEEEEE");

                //Change color of the fields to grey.
                companyName.Background = GreyBrush;
                companyBranche.Background = GreyBrush;
                companyCity.Background = GreyBrush;
                companyStreet.Background = GreyBrush;
                companyHouseNumber.Background = GreyBrush;
                companyHouseNumberAdd.Background = GreyBrush;
                companyCountry.Background = GreyBrush;
                companyPostalCode.Background = GreyBrush;
                companyPhoneNumber.Background = GreyBrush;
                companyMail.Background = GreyBrush;
                companyWebsite.Background = GreyBrush;*/

                //Disable the fields. 
                companyName.IsEnabled = false;
                companyBranche.IsEnabled = false;
                companyCity.IsEnabled = false;
                companyStreet.IsEnabled = false;
                companyHouseNumber.IsEnabled = false;
                companyHouseNumberAdd.IsEnabled = false;
                companyCountry.IsEnabled = false;
                companyPostalCode.IsEnabled = false;
                companyPhoneNumber.IsEnabled = false;
                companyMail.IsEnabled = false;
                companyWebsite.IsEnabled = false;

                //Clear the content of the fields.
                clearCompanyFields();
            }
        }

        //Toggle the editing mode of the company fields.
        private void toggleStudentEditMode()
        {
            if ((bool)CheckBox_Graduate.IsChecked)
            {
                TextBox_StudentName.IsEnabled = true;
                ListBox_Students.IsEnabled = true;
            }
            else
            {
                TextBox_StudentName.IsEnabled = false;
                ListBox_Students.IsEnabled = false;
                clearStudentFields();
            }
        }

        //Clear all the company fields.
        private void clearCompanyFields()
        {
            companyName.Text = null;
            companyBranche.Text = null;
            companyCity.Text = null;
            companyStreet.Text = null;
            companyHouseNumber.Text = null;
            companyHouseNumberAdd.Text = null;
            companyCountry.Text = null;
            companyPostalCode.Text = null;
            companyPhoneNumber.Text = null;
            companyMail.Text = null;
            companyWebsite.Text = null;
        }

        //Clear all the company fields.
        private void clearStudentFields()
        {
            TextBox_StudentName.Text = null;
            ListBox_Students.SelectedIndex = -1;
        }

    }
}
