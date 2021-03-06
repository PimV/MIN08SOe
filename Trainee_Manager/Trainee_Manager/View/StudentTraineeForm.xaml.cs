﻿using System;
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
        private int stageId;
        private int periodeId;
        String bedrijfID;

        private MainWindow mainWindow;
        
        private DataTable dataTable;

        Dictionary<int, string> dicAllStudents = new Dictionary<int, string>();
        Dictionary<int, string> dicSearchStudents = new Dictionary<int, string>();

        Dictionary<int, string> dicAllCompanys = new Dictionary<int, string>();
        Dictionary<int, string> dicSearchCompanys = new Dictionary<int, string>();


        public StudentTraineeForm(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            checkData(); 
        }

        private void getSubjectData()
        {
            //Fill the listbox containing ALL subjects
            DataTable tempTable = DatabaseConnection.commandSelect("CALL procedure_stage_kenmerken_not(" + stageId + ", " + Session.CourseID + ");");
            listbox_SubjectAll.SelectedValuePath = "id";
            listbox_SubjectAll.DisplayMemberPath = "naam";
            listbox_SubjectAll.ItemsSource = tempTable.DefaultView;

            //Fill the listbox containing trainee subjects
            tempTable = DatabaseConnection.commandSelect("CALL procedure_stage_kenmerken(" + stageId + ");");
            listbox_SubjectChosen.SelectedValuePath = "id";
            listbox_SubjectChosen.DisplayMemberPath = "naam";
            listbox_SubjectChosen.ItemsSource = tempTable.DefaultView;

        }

        //Gets companies from databse and fills listbox_Company with them.
        private void getCompanyData()
        {

            DataTable tempTable = DatabaseConnection.commandSelect("SELECT * FROM bedrijven WHERE verwijderd <> 1 ORDER BY naam ASC;");

            //Clear the dictionarys and listbox
            dicAllCompanys.Clear();
            dicSearchCompanys.Clear();
            listbox_Company.Items.Clear();

            foreach (DataRow row in tempTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string text = row["naam"].ToString();

                //Add the text and id to the dictionary for later use
                dicAllCompanys.Add(id, text);
                dicSearchCompanys.Add(id, text);

                listbox_Company.Items.Add(text);
            }
        }

        //Gets students from databse and fills listbox_Student with them.
        private void getStudentData()
        {
            String student2nr = textbox_studentNr.Text;
            if (student2nr == "")
            {
                student2nr = "null";
            }
            DataTable tempTable = DatabaseConnection.commandSelect("CALL procedure_student_form_getStudents(" + Session.ID + ", " + student2nr + ", " + periodeId + ");");

            //Clear the dictionarys and listbox
            dicAllStudents.Clear();
            dicSearchStudents.Clear();
            ListBox_Student.Items.Clear();

            foreach (DataRow row in tempTable.Rows)
            {
                int id = Convert.ToInt32(row["studentNr"]);
                string text = row["naam"].ToString();

                //Add the text and id to the dictionary for later use
                dicAllStudents.Add(id, text);
                dicSearchStudents.Add(id, text);

                ListBox_Student.Items.Add(text);
            }
        }

        private void otherCheckBox_Click(object sender, RoutedEventArgs e)
        {
            updateCompanyEditMode();

            //Clear the content of the fields.
            clearCompanyFields();
        }

        private void checkBoxEps_Click(object sender, RoutedEventArgs e)
        {
            updateEditMode();
        }

        private void graduateCheckBox_Click(object sender, RoutedEventArgs e)
        {
            updateStudentEditMode();

            //Clear the student fields.
            clearStudentFields();
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
            }
        }

        //Toggle the editing mode of the company fields.
        private void updateStudentEditMode()
        {
            if ((bool)CheckBox_Graduate.IsChecked)
            {
                TextBox_StudentSearch.IsEnabled = true;
                ListBox_Student.IsEnabled = true;
            }
            else
            {
                TextBox_StudentSearch.IsEnabled = false;
                ListBox_Student.IsEnabled = false;
            }
        }

        /**
         * This method checks if certain fields have certain values. Global editing mode is then turned on or off.
         * It also leaves the EPS checkbox enabled if that is the only cause of the editing mode to turn off. 
         */
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
                TextBox_StudentSearch.IsEnabled = false;
                ListBox_Student.IsEnabled = false;
                textBox_CompanyInstructor.IsEnabled = false;
                textBox_CompanyInstructorPhone.IsEnabled = false;
                textBox_CompanyInstructorMail.IsEnabled = false;
                CheckBox_Graduate.IsEnabled = false;
                textBox_Assignment.IsEnabled = false;
                textBox_OtherSubject.IsEnabled = false;
                listbox_SubjectAll.IsEnabled = false;
                listbox_SubjectChosen.IsEnabled = false;
                listbox_SubjectAll.SelectedIndex = -1;
                listbox_SubjectChosen.SelectedIndex = -1;
                button_SubjectAdd.IsEnabled = false;
                button_SubjectRemove.IsEnabled = false;
                button_SubjectNew.IsEnabled = false;

                if ((bool)checkBox_eps.IsChecked && 
                    label_Instructor.Content.Equals("Niet toegekend") &&
                    !(bool)checkBox_ApprovalAssignment.IsChecked)
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
                listbox_SubjectAll.IsEnabled = true;
                listbox_SubjectChosen.IsEnabled = true;
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

            bedrijfID = "null";
        }

        //Clear all the company fields.
        private void clearStudentFields()
        {
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
            listbox_Company.SelectedIndex = -1;
            textBox_CompanyInstructor.Text = null;
            textBox_CompanyInstructorPhone.Text = null;
            textBox_CompanyInstructorMail.Text = null;
            CheckBox_Graduate.IsChecked = false;
            textBox_Assignment.Text = null;
            textBox_OtherSubject.Text = null;
        }

        //Check if the student has been connected to a trainee period. 
        private void checkData()
        {
            dataTable = DatabaseConnection.commandSelect("procedure_student_check_ingeschreven(" + Session.ID + ");");
            foreach (DataRow row in dataTable.Rows)
            {
                if (Convert.ToInt32(row["aantal"]) < 1)
                {
                    MessageBox.Show("Er staan geen stages geregistreerd op uw naam.");
                    mainWindow.showLoginScreen();
                }
            }
        }

        //Get and show all the trainee information. 
        public void showPeriod(int periodeId)
        {
            //Aantal waardes en velden op default zetten.
            clearAllFields();
            stageId = -1;
            bedrijfID = "null";
            checkBox_PermissionTraineeship.IsChecked = false;
            checkBox_ApprovalAssignment.IsChecked = false;
            label_Instructor.Content = "Niet toegekend";
            label_Reader.Content = "n.v.t";

            this.periodeId = periodeId;
            
            dataTable = DatabaseConnection.commandSelect("CALL procedure_student_form(" + periodeId + ", " + Session.ID + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                stageId = Convert.ToInt32(row["stage_id"]);
                bedrijfID = row["bedrijf_id"].ToString();

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

                //Show specific strings in the instructor and reader labels in certain circumstances.
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

            }

            getCompanyData();
            getStudentData();
            getSubjectData();
            updateEditMode();
        }


        public void save()
        {
            //Don't check fields when EPS is checked. 
            if (!(bool)checkBox_eps.IsChecked)
            {
                //Check if company fields are filled in. 
                if (textBox_CompanyName.Text.Equals("") ||
                    textBox_CompanyCity.Text.Equals("") ||
                    textBox_CompanyStreet.Text.Equals("") ||
                    textBox_CompanyHouseNumber.Text.Equals("") ||
                    textBox_CompanyPostalCode.Text.Equals(""))
                {
                    MessageBox.Show("Geen bedrijf opgegeven! \r\nKies een bedrijf, of voeg er een toe waarvan op zijn minst de naam, plaats, straat, nummer en postcode is ingevuld.");
                    return;
                }

                //Check if co-student has been selected when a graduate-traineeship has been selected.
                if ((Boolean)CheckBox_Graduate.IsChecked && textbox_studentNr.Text.Equals(""))
                {
                    MessageBox.Show("Kies een mede student! \r\nWanneer deze niet in de lijst staat heeft deze mogelijk zelf al een stage formulier ingevuld. \r\nIs dit niet het geval, contacteer dan de stage coördinator.");
                    return;
                }

                //If a company has been selected in the listbox, get its ID. 
                if (listbox_Company.SelectedValue != null)
                {
                    bedrijfID = dicSearchCompanys.ElementAt(listbox_Company.SelectedIndex).Key.ToString();
                }

                //If the 'new company' checkbox has been selected, create a new company. 
                if ((bool)checkBox_NewCompany.IsChecked)
                {
                    //Create new company and get his ID returned. 
                    dataTable = DatabaseConnection.commandSelect("CALL procedure_bedrijf_add('" + textBox_CompanyName.Text + "','" + textBox_CompanyBranche.Text + "','" + textBox_CompanyCity.Text + "','" + textBox_CompanyStreet.Text + "','" + textBox_CompanyHouseNumber.Text + "','" + textBox_CompanyHouseNumberAdd.Text + "','" + textBox_CompanyCountry.Text + "','" + textBox_CompanyPostalCode.Text + "','" + textBox_CompanyPhoneNumber.Text + "','" + textBox_CompanyWebsite.Text + "', null);");

                    //Get the ID of the created company. 
                    foreach (DataRow row in dataTable.Rows)
                    {
                        bedrijfID = row["ID"].ToString();
                    }
                }
            }

            //Save the trainee form to the database. 
            String student1nr = Session.ID.ToString();
            String student2nr = textbox_studentNr.Text;
            if (student2nr == "")
            {
                student2nr = "null";
            }
            if (bedrijfID == "")
            {
                bedrijfID = "null";
            }

            dataTable = DatabaseConnection.commandSelect("CALL procedure_student_form_save(" + stageId + "," + periodeId + ", " + checkBox_eps.IsChecked + "," + bedrijfID + ",'" + textBox_CompanyInstructor.Text + "','" + textBox_CompanyInstructorPhone.Text + "','" + textBox_CompanyInstructorMail.Text + "'," + CheckBox_Graduate.IsChecked + "," + student1nr + "," + student2nr + ",'" + textBox_Assignment.Text + "');");

            //Get the trainee ID if it has just been newly created (not updated)
            if (dataTable.Rows.Count > 0)
            {
                stageId = Convert.ToInt32(dataTable.Rows[0]["ID"]);
            }

            showPeriod(periodeId);
            MessageBox.Show("Gegevens opgeslagen.");
        }

        private void listBox_Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listbox_Company.SelectedItem != null)
            {
                var row = dicSearchCompanys.ElementAt(listbox_Company.SelectedIndex);

                //Call the procedure to load the mysql data
                dataTable = DatabaseConnection.commandSelect("SELECT * FROM bedrijven WHERE id = " + row.Key + "");

                foreach (DataRow record in dataTable.Rows)
                {
                    textBox_CompanyName.Text = record["naam"].ToString();
                    textBox_CompanyBranche.Text = record["branche"].ToString();
                    textBox_CompanyCity.Text = record["plaats"].ToString();
                    textBox_CompanyStreet.Text = record["straat"].ToString();
                    textBox_CompanyHouseNumber.Text = record["nummer"].ToString();
                    textBox_CompanyCountry.Text = record["land"].ToString();
                    textBox_CompanyPostalCode.Text = record["postcode"].ToString();
                    textBox_CompanyPhoneNumber.Text = record["telefoonnummer"].ToString();
                    textBox_CompanyWebsite.Text = record["website"].ToString();
                }
            }
        }

        private void listBox_Student_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox_Student.SelectedIndex > -1)
            {
                var row = dicSearchStudents.ElementAt(ListBox_Student.SelectedIndex);

                if ((bool)CheckBox_Graduate.IsChecked)
                {
                    textbox_studentName.Text = row.Value;
                    textbox_studentNr.Text = row.Key.ToString();
                }
                
            }
        }

        private void button_subjectAdd_Click(object sender, RoutedEventArgs e)
        {            
            if (listbox_SubjectAll.SelectedItem != null)
            {
                if (stageId < 0)
                {
                    MessageBox.Show("Sla eerst de stage een keer op. Daarna kunt u kenmerken toevoegen.");
                    return;
                }
                DataRowView selection = (DataRowView)listbox_SubjectAll.SelectedItem;
                int id = Convert.ToInt32(selection.Row["id"]);
                Console.Write(stageId);

                DataTable tempTable = DatabaseConnection.commandSelect("CALL procedure_stage_kenmerken_add(" + stageId + "," + id + ");");
                getSubjectData();
            }
        }

        private void button_subjectDelete_Click(object sender, RoutedEventArgs e)
        {            
            if (listbox_SubjectChosen.SelectedItem != null)
            {
                if (stageId < 0)
                {
                    MessageBox.Show("Sla eerst de stage een keer op. Daarna kunt u kenmerken toevoegen.");
                    return;
                }
                DataRowView selection = (DataRowView)listbox_SubjectChosen.SelectedItem;
                int id = Convert.ToInt32(selection.Row["id"]);
                

                DataTable tempTable = DatabaseConnection.commandSelect("CALL procedure_stage_kenmerken_del(" + stageId + "," + id + ");");
                getSubjectData();
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

        private void textbox_StudentSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string search = TextBox_StudentSearch.Text.Trim().ToLower();

            ListBox_Student.Items.Clear();
            dicSearchStudents.Clear();
            foreach (var record in dicAllStudents)
            {
                if (record.Value.Trim().ToLower().Contains(search))
                {
                    dicSearchStudents.Add(record.Key, record.Value);
                    ListBox_Student.Items.Add(record.Value);
                }
            }
        }

        private void textbox_CompanySearch_KeyUp(object sender, KeyEventArgs e)
        {
            string search = textbox_CompanySearch.Text.Trim().ToLower();

            listbox_Company.Items.Clear();
            dicSearchCompanys.Clear();
            foreach (var record in dicAllCompanys)
            {
                if (record.Value.Trim().ToLower().Contains(search))
                {
                    dicSearchCompanys.Add(record.Key, record.Value);
                    listbox_Company.Items.Add(record.Value);
                }
            }
        }
        
        private void button_SubjectNew_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_OtherSubject.Text.Trim() != "")
            {
                dataTable = DatabaseConnection.commandSelect("CALL procedure_kenmerken_add('" + textBox_OtherSubject.Text.Trim() + "', " + Session.CourseID + ");");
                foreach (DataRow row in dataTable.Rows)
                {
                    if (!row.IsNull("id"))
                    {
                        dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_kenmerken_add(" + stageId + "," + row["id"].ToString() + ");");                    
                    }
                }
                getSubjectData();
                textBox_OtherSubject.Clear();
            }
        }

    }
}
