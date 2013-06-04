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
        
        private DataTable dataTable;

        public StudentTraineeForm(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            checkData(); 
            getCompanyData();
            getStudentData();
        }

        //Gets companies from databse and fills listbox_Company with them.
        private void getCompanyData()
        {
            DataTable tempTable = DatabaseConnection.commandSelect("SELECT * FROM bedrijven");
            dataTable = DatabaseConnection.commandSelect("SELECT * FROM bedrijven");

            listbox_Company.SelectedValuePath = "id";
            listbox_Company.DisplayMemberPath = "naam";
            listbox_Company.ItemsSource = tempTable.DefaultView;
        }

        //Gets students from databse and fills listbox_Student with them.
        private void getStudentData()
        {
            DataTable tempTable = DatabaseConnection.commandSelect("SELECT * FROM studenten");
            ListBox_Student.SelectedValuePath = "id";
            ListBox_Student.DisplayMemberPath = "achternaam";
            ListBox_Student.ItemsSource = tempTable.DefaultView;
        }

        private void otherCheckBox_Click(object sender, RoutedEventArgs e)
        {
            updateCompanyEditMode();
        }

        private void checkBoxEps_Click(object sender, RoutedEventArgs e)
        {
            updateEditMode();
        }

        private void graduateCheckBox_Click(object sender, RoutedEventArgs e)
        {
            updateStudentEditMode();
        }

        //Toggle the editing mode of the company fields.
        private void updateCompanyEditMode()
        {
            //Turn on edit-mode.
            if ((bool)checkBox_NewCompany.IsChecked)
            {
                //Enable the company fields.
                textBox_CompanyName.IsEnabled = true;
                textBox_CompanyBranche.IsEnabled = true;
                textBox_CompanyCity.IsEnabled = true;
                textBox_CompanyStreet.IsEnabled = true;
                textBox_CompanyHouseNumber.IsEnabled = true;
                textBox_CompanyHouseNumberAdd.IsEnabled = true;
                textBox_CompanyCountry.IsEnabled = true;
                textBox_CompanyPostalCode.IsEnabled = true;
                textBox_CompanyPhoneNumber.IsEnabled = true;
                textBox_CompanyWebsite.IsEnabled = true;

                listbox_Company.SelectedIndex = -1;
                textbox_CompanySearch.Text = null;
                listbox_Company.IsEnabled = false;
                textbox_CompanySearch.IsEnabled = false;
            }
            //turn off edit-mode 
            else
            {
                //Disable the fields. 
                textBox_CompanyName.IsEnabled = false;
                textBox_CompanyBranche.IsEnabled = false;
                textBox_CompanyCity.IsEnabled = false;
                textBox_CompanyStreet.IsEnabled = false;
                textBox_CompanyHouseNumber.IsEnabled = false;
                textBox_CompanyHouseNumberAdd.IsEnabled = false;
                textBox_CompanyCountry.IsEnabled = false;
                textBox_CompanyPostalCode.IsEnabled = false;
                textBox_CompanyPhoneNumber.IsEnabled = false;
                textBox_CompanyWebsite.IsEnabled = false;
                listbox_Company.IsEnabled = true;
                textbox_CompanySearch.IsEnabled = true;

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
                ListBox_Student.IsEnabled = true;
            }
            else
            {
                TextBox_Student.IsEnabled = false;
                ListBox_Student.IsEnabled = false;
                clearStudentFields();
            }
        }

        //Toggle global editing mode.
        private void updateEditMode()
        {
            if ((bool)checkBox_eps.IsChecked ||
                !label_Instructor.Content.Equals("Niet toegekend") ||
                (bool)checkBox_ApprovalAssignment.IsChecked)
            {
                //Disable all fields. 
                checkBox_NewCompany.IsEnabled = false;
                checkBox_eps.IsEnabled = false;
                textbox_CompanySearch.IsEnabled = false;
                listbox_Company.IsEnabled = false;
                TextBox_Student.IsEnabled = false;
                ListBox_Student.IsEnabled = false;
                textBox_CompanyInstructor.IsEnabled = false;
                textBox_CompanyInstructorPhone.IsEnabled = false;
                textBox_CompanyInstructorMail.IsEnabled = false;
                CheckBox_Graduate.IsEnabled = false;
                textBox_Assignment.IsEnabled = false;
                textBox_OtherSubject.IsEnabled = false;
                listBox_SubjectsLeft.IsEnabled = false;
                listBox_SubjectsRight.IsEnabled = false;
                listBox_SubjectsLeft.SelectedIndex = -1;
                listBox_SubjectsRight.SelectedIndex = -1;
                button_SubjectAdd.IsEnabled = false;
                button_SubjectRemove.IsEnabled = false;
                button_SubjectNew.IsEnabled = false;

                if ((bool)checkBox_eps.IsChecked)
                {
                    clearAllFields();
                    checkBox_eps.IsEnabled = true;
                    checkBox_eps.IsChecked = true;
                }
            }
            else
            {
                //Enable all fields.
                checkBox_NewCompany.IsEnabled = true;
                checkBox_eps.IsEnabled = true;
                updateCompanyEditMode();
                updateStudentEditMode();
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
            textBox_CompanyName.Text = null;
            textBox_CompanyBranche.Text = null;
            textBox_CompanyCity.Text = null;
            textBox_CompanyStreet.Text = null;
            textBox_CompanyHouseNumber.Text = null;
            textBox_CompanyHouseNumberAdd.Text = null;
            textBox_CompanyCountry.Text = null;
            textBox_CompanyPostalCode.Text = null;
            textBox_CompanyPhoneNumber.Text = null;
            textBox_CompanyWebsite.Text = null;
        }

        //Clear all the company fields.
        private void clearStudentFields()
        {
            TextBox_Student.Text = null;
            ListBox_Student.SelectedIndex = -1;
            textbox_studentName.Text = null;
            textbox_studentNr.Text = null;
        }

        //Clear all of the form fields. (used when 'eps' is selected)
        private void clearAllFields()
        {
            clearCompanyFields();
            clearStudentFields();

            checkBox_NewCompany.IsChecked = false;
            checkBox_eps.IsChecked = false;
            textbox_CompanySearch.Text = null;
            listbox_Company.SelectedIndex = -1;
            textBox_CompanyInstructor.Text = null;
            textBox_CompanyInstructorPhone.Text = null;
            textBox_CompanyInstructorMail.Text = null;
            CheckBox_Graduate.IsChecked = false;
            textBox_Assignment.Text = null;
            textBox_OtherSubject.Text = null;
            //TODO: Move all items from listBox_SubjectRight to listBox_SubjectLeft.
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
            dataTable = DatabaseConnection.commandSelect("CALL procedure_student_form(" + stageID + ", " + Session.ID + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                textBox_CompanyName.Text = row["bedrijf"].ToString();
                textBox_CompanyBranche.Text = row["branche"].ToString();
                textBox_CompanyCity.Text = row["locatie"].ToString();
                textBox_CompanyStreet.Text = row["straat"].ToString();
                textBox_CompanyHouseNumber.Text = row["nummer"].ToString();
                textBox_CompanyHouseNumberAdd.Text = row["toevoeging"].ToString();
                textBox_CompanyCountry.Text = row["land"].ToString();
                textBox_CompanyPostalCode.Text = row["postcode"].ToString();
                textBox_CompanyPhoneNumber.Text = row["telefoonnummer"].ToString();
                textBox_CompanyWebsite.Text = row["website"].ToString();

                textbox_studentName.Text = row["student"].ToString();
                textbox_studentNr.Text = row["studentnummer"].ToString();

                checkBox_eps.IsChecked = (Boolean)row["eps"];
                textBox_CompanyInstructor.Text = row["bedrijfsbegeleider"].ToString();
                textBox_CompanyInstructorPhone.Text = row["bedrijfsbegeleider_tel"].ToString();
                textBox_CompanyInstructorMail.Text = row["bedrijfsbegeleider_email"].ToString();
                CheckBox_Graduate.IsChecked = (Boolean)row["afstudeerstage"];
                textBox_Assignment.Text = row["opdracht"].ToString();

                if (row["begeleider"].ToString().Equals(""))
                {
                    label_Instructor.Content = "Niet toegekend";
                }
                else
                {
                    label_Instructor.Content = row["begeleider"].ToString();
                }

                if (!(Boolean)row["afstudeerstage"])
                {
                    label_Reader.Content = "n.v.t";
                }
                else if (!row["lezer"].ToString().Equals(""))
                {
                    label_Reader.Content = row["lezer"].ToString();
                }
                else
                {
                    label_Reader.Content = "Niet toegekend";
                }
                
                checkBox_PermissionTraineeship.IsChecked = (Boolean)row["toestemming"];
                checkBox_ApprovalAssignment.IsChecked = (Boolean)row["goedkeuring"];

                updateEditMode();
            }
        }


        public void save()
        {
            String bedrijfID = "";
            if (listbox_Company.SelectedValue != null)
            {
                bedrijfID = listbox_Company.SelectedValue.ToString();
            }

            if ((bool)checkBox_NewCompany.IsChecked)
            {
                //Nieuw bedrijf aanmaken
                dataTable = DatabaseConnection.commandSelect("CALL procedure_bedrijf_add('" + textBox_CompanyName.Text + "','" + textBox_CompanyBranche.Text + "','" + textBox_CompanyCity.Text + "','" + textBox_CompanyStreet.Text + "','" + textBox_CompanyHouseNumber.Text + "','" + textBox_CompanyHouseNumberAdd.Text + "','" + textBox_CompanyCountry.Text + "','" + textBox_CompanyPostalCode.Text + "','" + textBox_CompanyPhoneNumber.Text + "','" + textBox_CompanyWebsite.Text + "');");

                foreach (DataRow row in dataTable.Rows)
                {
                    bedrijfID = row["ID"].ToString();
                }
            }


            //Todo: pagina herladen. Hier hebben we het stage ID voor nodig. 
            //showPeriod();
        }

        private void listBox_Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox_Company.SelectedItem != null)
            {
                
            }
            DataRowView selection = (DataRowView)listbox_Company.SelectedItem;
            
            textBox_CompanyName.Text = selection.Row["naam"].ToString();
            textBox_CompanyBranche.Text = selection.Row["branche"].ToString();
            textBox_CompanyCity.Text = selection.Row["plaats"].ToString();
            textBox_CompanyStreet.Text = selection.Row["straat"].ToString();
            textBox_CompanyHouseNumber.Text = selection.Row["nummer"].ToString();
            textBox_CompanyCountry.Text = selection.Row["land"].ToString();
            textBox_CompanyPostalCode.Text = selection.Row["postcode"].ToString();
            textBox_CompanyPhoneNumber.Text = selection.Row["telefoonnummer"].ToString();
            textBox_CompanyWebsite.Text = selection.Row["website"].ToString();

            Console.WriteLine("This is the " + selection.Row["plaats"].ToString());
        }

        private void listBox_Student_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selection = (DataRowView)ListBox_Student.SelectedItem;

            if ((bool)CheckBox_Graduate.IsChecked)
            {
                textbox_studentName.Text = selection.Row["roepnaam"].ToString() + " " + selection.Row["achternaam"].ToString();
                textbox_studentNr.Text = selection.Row["studentnr"].ToString();
            }
        }

        private void textbox_CompanySearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(textbox_CompanySearch.Text))
            {
                DataTable tempTable = dataTable;
                listbox_Company.SelectedValuePath = "id";
                listbox_Company.DisplayMemberPath = "naam";
                listbox_Company.ItemsSource = tempTable.DefaultView;
            }
            else
            {
                DataTable tempTable = dataTable;
                listbox_Company.SelectedValuePath = "id";
                listbox_Company.DisplayMemberPath = "naam";
                listbox_Company.ItemsSource = tempTable.DefaultView;
            }
        }
    }
}
