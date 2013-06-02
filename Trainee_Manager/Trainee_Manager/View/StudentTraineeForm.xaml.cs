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
    /// Interaction logic for StudentTraineeForm.xaml
    /// </summary>
    public partial class StudentTraineeForm : UserControl
    {

        private MainWindow mainWindow;
        private Boolean editMode;
        public Boolean EditMode
        {
            get { return editMode; }
            set 
            { 
                editMode = value;
                updateEditMode();
            }
        }
        
        private DataTable dataTable;

        public StudentTraineeForm(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            checkData();
            EditMode = true;
        }

        private void otherCheckBox_Click(object sender, RoutedEventArgs e)
        {
            updateCompanyEditMode();
        }

        private void checkBoxEps_Click(object sender, RoutedEventArgs e)
        {
            updateEpsMode();
        }

        private void graduateCheckBox_Click(object sender, RoutedEventArgs e)
        {
            updateStudentEditMode();
        }

        //Turn on/off EPS mode depending on its checkbox. (all readonly except EPS checkbox)
        private void updateEpsMode()
        {
            Boolean epsEnabled = (bool)checkBox_eps.IsChecked;
            if (epsEnabled)
            {
                EditMode = false;
                clearAllFields();
                checkBox_eps.IsEnabled = true;
                checkBox_eps.IsChecked = true;
            }
            else
            {
                EditMode = true;
            }
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
        private void updateEditMode()
        {
            if (!EditMode)
            {
                //Clear all fields. 
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
            textbox_StudentNumber.Text = null;
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

        //Check if the student has any 
        private void checkData()
        {

            //Check to see if the user has any trainee records. 
            dataTable = DatabaseConnection.commandSelect("SELECT COUNT(*) AS aantal " +
                                                         "FROM " +
                                                         "stages " +
                                                         "LEFT JOIN studenten as stu ON stages.student_id = stu.id " +
                                                         "LEFT JOIN studenten as stu2 ON stages.student_id_2 = stu2.id " +
                                                         "WHERE " +
                                                         "stu.studentnr = " + Session.ID + " OR stu2.studentnr = " + Session.ID);
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["aantal"].ToString() == "0")
                {
                    MessageBox.Show("Er staan geen stages geregistreerd op uw naam.");
                    mainWindow.showLoginScreen();
                }
            }
        }

        public void showPeriod(int stageID)
        {
            Boolean parsed;
            dataTable = DatabaseConnection.commandSelect("CALL procedure_student_form(" + stageID + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                companyName.Text = row["bedrijf"].ToString();
                companyBranche.Text = row["branche"].ToString();
                companyCity.Text = row["locatie"].ToString();
                companyStreet.Text = row["straat"].ToString();
                companyHouseNumber.Text = row["nummer"].ToString();
                companyHouseNumberAdd.Text = row["toevoeging"].ToString();
                companyCountry.Text = row["land"].ToString();
                companyPostalCode.Text = row["postcode"].ToString();
                companyPhoneNumber.Text = row["telefoonnummer"].ToString();
                //companyMail.Text = row["bedrijf"].ToString();
                companyWebsite.Text = row["website"].ToString();

                textbox_StudentName.Text = row["student2"].ToString();
                textbox_StudentNumber.Text = row["studentnummer2"].ToString();

                checkBox_eps.IsChecked = (Boolean)row["eps"];
                textBox_CompanyInstructor.Text = row["bedrijfsbegeleider"].ToString();
                textBox_CompanyInstructorPhone.Text = row["bedrijfsbegeleider_tel"].ToString();
                textBox_CompanyInstructorMail.Text = row["bedrijfsbegeleider_email"].ToString();
                CheckBox_Graduate.IsChecked = (Boolean)row["afstudeerstage"];
                textBox_Assignment.Text = row["opdracht"].ToString();

                if (row["begeleider"].ToString() == "")
                {
                    label_Instructor.Content = "Niet toegekend";
                }
                else
                {
                    label_Instructor.Content = row["begeleider"].ToString();
                }

                if (!Boolean.Parse(row["afstudeerstage"].ToString()))
                {
                    label_Reader.Content = "n.v.t";
                }
                else if (row["lezer"].ToString() == "")
                {
                    label_Reader.Content = row["lezer"].ToString();
                }
                else
                {
                    label_Reader.Content = "Niet toegekend";
                }
                
                checkBox_PermissionTraineeship.IsChecked = (Boolean)row["toestemming"];
                checkBox_ApprovalAssignment.IsChecked = (Boolean)row["goedkeuring"];

                updateStudentEditMode();
                updateEpsMode();

                if ((Boolean)row["goedkeuring"] || row["begeleider"].ToString() != "")
                {
                    EditMode = false;
                }
                else if (!Boolean.TryParse(row["eps"].ToString(), out parsed))
                {
                    EditMode = true;
                }
            }
        }
    }
}
