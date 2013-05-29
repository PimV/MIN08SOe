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
        Boolean editMode;

        public StudentTraineeForm(int id)
        {
            InitializeComponent();
            this.id = id;
            Console.WriteLine("IntStudentTraineeForm: " + this.id);
            editMode = true;
        }

        private void otherCheckBox_Click(object sender, RoutedEventArgs e)
        {
            updateCompanyEditMode();
        }

        private void checkBoxEps_Click(object sender, RoutedEventArgs e)
        {
            Boolean epsEnabled = (bool)checkBox_eps.IsChecked;
            toggleEditMode();
            if (epsEnabled)
            {
                checkBox_eps.IsEnabled = true;
                checkBox_eps.IsChecked = true;
            }
            
        }

        private void graduateCheckBox_Click(object sender, RoutedEventArgs e)
        {
            updateStudentEditMode();
        }

        //Toggle the editing mode of the company fields.
        private void updateCompanyEditMode()
        {
            //Turn on edit-mode.
            if ((bool)otherCheckBox.IsChecked)
            {
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

                listbox_Company.SelectedIndex = -1;
                textbox_Company.Text = null;
                listbox_Company.IsEnabled = false;
                textbox_Company.IsEnabled = false;
            }
            //turn off edit-mode 
            else
            {
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
                listbox_Company.IsEnabled = true;
                textbox_Company.IsEnabled = true;

                //Clear the content of the fields.
                clearCompanyFields();
            }
        }

        //Toggle the editing mode of the company fields.
        private void updateStudentEditMode()
        {
            if ((bool)CheckBox_Graduate.IsChecked)
            {
                TextBox_Student.IsEnabled = true;
                ListBox_Students.IsEnabled = true;
            }
            else
            {
                TextBox_Student.IsEnabled = false;
                ListBox_Students.IsEnabled = false;
                clearStudentFields();
            }
        }

        //Toggle global editing mode.
        private void toggleEditMode()
        {
            if (editMode)
            {
                //Clear and disable all fields. 
                clearAllFields();

                otherCheckBox.IsEnabled = false;
                checkBox_eps.IsEnabled = false;
                textbox_Company.IsEnabled = false;
                listbox_Company.IsEnabled = false;
                textBox_CompanyInstructor.IsEnabled = false;
                textBox_CompanyInstructorPhone.IsEnabled = false;
                textBox_CompanyInstructorMail.IsEnabled = false;
                CheckBox_Graduate.IsEnabled = false;
                textBox_Assignment.IsEnabled = false;
                textBox_OtherSubject.IsEnabled = false;
                listBox_SubjectsLeft.IsEnabled = false;
                listBox_SubjectsRight.IsEnabled = false;
                button_SubjectAdd.IsEnabled = false;
                button_SubjectRemove.IsEnabled = false;
                button_SubjectNew.IsEnabled = false;

                editMode = false;
            }
            else
            {
                //Enable all fields.
                otherCheckBox.IsEnabled = true;
                checkBox_eps.IsEnabled = true;
                textbox_Company.IsEnabled = true;
                listbox_Company.IsEnabled = true;
                textBox_CompanyInstructor.IsEnabled = true;
                textBox_CompanyInstructorPhone.IsEnabled = true;
                textBox_CompanyInstructorMail.IsEnabled = true;
                CheckBox_Graduate.IsEnabled = true;
                textBox_Assignment.IsEnabled = true;
                textBox_OtherSubject.IsEnabled = true;
                listBox_SubjectsLeft.IsEnabled = true;
                listBox_SubjectsRight.IsEnabled = true;
                button_SubjectAdd.IsEnabled = true;
                button_SubjectRemove.IsEnabled = true;
                button_SubjectNew.IsEnabled = true;

                editMode = true;
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
            TextBox_Student.Text = null;
            ListBox_Students.SelectedIndex = -1;
            textbox_StudentName.Text = null;
            textbox_StudentNumber = null;
        }

        //Clear all of the form fields. (used when 'eps' is selected)
        private void clearAllFields()
        {
            clearCompanyFields();
            clearStudentFields();

            otherCheckBox.IsChecked = false;
            checkBox_eps.IsChecked = false;
            textbox_Company.Text = null;
            listbox_Company.SelectedIndex = -1;
            textBox_CompanyInstructor.Text = null;
            textBox_CompanyInstructorPhone.Text = null;
            textBox_CompanyInstructorMail.Text = null;
            CheckBox_Graduate.IsChecked = false;
            textBox_Assignment.Text = null;
            textBox_OtherSubject.Text = null;
            //TODO: Move all items from listBox_SubjectRight to listBox_SubjectLeft.

            updateCompanyEditMode();
            updateStudentEditMode();
        }

    }
}
