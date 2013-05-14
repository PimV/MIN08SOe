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
        public StudentTraineeForm()
        {
            InitializeComponent();
        }

        private void otherCheckBox_Click(object sender, RoutedEventArgs e)
        {
            toggleCompanyEditMode();
        }

        private void toggleCompanyEditMode()
        {
            if ((bool)otherCheckBox.IsChecked)
            {
                /*companyName.IsReadOnly = false;
                companyBranche.IsReadOnly = false;
                companyCity.IsReadOnly = false;
                companyStreet.IsReadOnly = false;
                companyHouseNumber.IsReadOnly = false;
                companyHouseNumberAdd.IsReadOnly = false;
                companyCountry.IsReadOnly = false;
                companyPostalCode.IsReadOnly = false;
                companyPhoneNumber.IsReadOnly = false;
                companyMail.IsReadOnly = false;
                companyWebsite.IsReadOnly = false;*/

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
                companyWebsite.Background = Brushes.White;

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
            else
            {
                /*companyName.IsReadOnly = true;
                companyBranche.IsReadOnly = true;
                companyCity.IsReadOnly = true;
                companyStreet.IsReadOnly = true;
                companyHouseNumber.IsReadOnly = true;
                companyHouseNumberAdd.IsReadOnly = true;
                companyCountry.IsReadOnly = true;
                companyPostalCode.IsReadOnly = true;
                companyPhoneNumber.IsReadOnly = true;
                companyMail.IsReadOnly = true;
                companyWebsite.IsReadOnly = true;*/

                BrushConverter bc = new BrushConverter();
                Brush GreyBrush = (Brush)bc.ConvertFrom("#FFEEEEEE");

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
                companyWebsite.Background = GreyBrush;

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

                clearCompanyFields();
            }
        }

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
    }
}
