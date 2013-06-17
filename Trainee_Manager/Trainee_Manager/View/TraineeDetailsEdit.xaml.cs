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
        private int stageId, bedrijfId, opleidingId, periodeId;
        private static DataTable dataTable;

        Dictionary<int, string> dicAllStudents = new Dictionary<int, string>();
        Dictionary<int, string> dicSearchStudents = new Dictionary<int, string>();

        Dictionary<int, string> dicAllCompanys = new Dictionary<int, string>();
        Dictionary<int, string> dicSearchCompanys = new Dictionary<int, string>();


        public TraineeDetailsEdit(int id)
        {
            InitializeComponent();
            stageId = id;

            setAllData();
            //getCompanyData();
            //getStudentData();
        }

        private void setAllData()
        {
            //Show all normal data
            getOveralData();

            //Show all companies in the listbox
            getCompanyData();

            //Show all students in the listbox
            getStudentData();

            //Show all subjects
            getSubjectData();
        }

        private void getOveralData()
        {
            //Call the procedure to load the mysql data
            dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_details_edit(" + stageId + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                opleidingId = Convert.ToInt32(row["opleiding_id"].ToString());
                periodeId = Convert.ToInt32(row["periode_id"].ToString());
                textbox_student1.Text = row["student1"].ToString();
                textbox_student1nummer.Text = row["studentnummer1"].ToString();

                if (!row["student2"].ToString().Equals(""))
                {
                    textbox_student2.Text = row["student2"].ToString();
                    textbox_student2nummer.Text = row["studentnummer2"].ToString();
                }
                else
                {
                    radioButton_Student2.IsEnabled = false;
                }

                textbox_opmerking.Text = row["opmerking"].ToString();
                textbox_opdracht.Text = row["opdracht"].ToString();


                //ja of nee ipv true or false
                if (row["afstudeerstage"].ToString().Equals("True"))
                {
                    checkbox_Afstudeer.IsChecked = true;
                }

                //ja of nee ipv true or false
                if (row["toestemming"].ToString().Equals("True"))
                {
                    checkbox_toestemming.IsChecked = true;
                }

                //ja of nee ipv true or false
                if (row["goedkeuring"].ToString().Equals("True"))
                {
                    checkbox_goedkeuring.IsChecked = true;
                }

                if (row["bedrijf_id"].ToString() != string.Empty)
                {
                    bedrijfId = Convert.ToInt32(row["bedrijf_id"].ToString());

                    textbox_bedrijf.Text = row["bedrijf"].ToString();
                    textbox_bedrijfplaats.Text = row["locatie"].ToString();
                    textbox_bedrijfadres.Text = row["adres"].ToString();

                    textbox_bedrijfsbegeleider.Text = row["bedrijfsbegeleider"].ToString();
                    textbox_bedrijfsbegeleideremail.Text = row["bedrijfsbegeleider_email"].ToString();
                    textbox_bedrijfsbegeleidertelefoon.Text = row["bedrijfsbegeleider_tel"].ToString();
                }
            }
        }

        private void getSubjectData()
        {
            //Fill the listbox containing ALL subjects
            DataTable tempTable = DatabaseConnection.commandSelect("CALL procedure_stage_kenmerken_not(" + stageId + "," + opleidingId + ");");
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
            //DataTable tempTable = DatabaseConnection.commandSelect("SELECT * FROM bedrijven");
            //listBox_Company.SelectedValuePath = "id";
            //listBox_Company.DisplayMemberPath = "naam";
            //listBox_Company.ItemsSource = tempTable.DefaultView;

            DataTable tempTable = DatabaseConnection.commandSelect("SELECT * FROM bedrijven WHERE verwijderd <> 1");

            foreach (DataRow row in tempTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string text = row["naam"].ToString();

                //Add the text and id to the dictionary for later use
                dicAllCompanys.Add(id, text);
                dicSearchCompanys.Add(id, text);

                listBox_Company.Items.Add(text);
            }
        }

        //Gets students from databse and fills listbox_Student with them.
        private void getStudentData()
        {
            String student1nr = textbox_student1nummer.Text;
            if (student1nr == "")
            {
                student1nr = "null";
            }
            String student2nr = textbox_student2nummer.Text;
            if (student2nr == "")
            {
                student2nr = "null";
            }

            DataTable tempTable = DatabaseConnection.commandSelect("CALL procedure_stage_details_edit_getstudents(" + student1nr + "," + student2nr + "," + periodeId + ");");

            foreach (DataRow row in tempTable.Rows)
            {
                int id = Convert.ToInt32(row["studentNr"]);
                string text = row["naam"].ToString();

                //Add the text and id to the dictionary for later use
                dicAllStudents.Add(id, text);
                dicSearchStudents.Add(id, text);

                listBox_Student.Items.Add(text);
            }
        }

        private void toggleStudentEditMode(object sender, RoutedEventArgs e)
        {
            if ((bool)checkbox_Afstudeer.IsChecked)
            {
                radioButton_Student2.IsEnabled = true;

                //Call the procedure to load the mysql data
                dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_details_edit(" + stageId + ");");

                foreach (DataRow row in dataTable.Rows)
                {
                    textbox_student2.Text = row["student2"].ToString();
                    textbox_student2nummer.Text = row["studentnummer2"].ToString();
                }
            }
            else
            {
                radioButton_Student1.IsChecked = true;
                radioButton_Student2.IsEnabled = false;
                textbox_student2.Text = "";
                textbox_student2nummer.Text = "";
            }
        }

        private void listBox_Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataRowView selection = (DataRowView)listBox_Company.SelectedItem;

            //textbox_bedrijf.Text = selection.Row["naam"].ToString();
            //textbox_bedrijfplaats.Text = selection.Row["straat"].ToString() + " " + selection.Row["nummer"].ToString();
            //textbox_bedrijfadres.Text = selection.Row["plaats"].ToString();

            if (listBox_Company.SelectedIndex > -1)
            {
                var row = dicSearchCompanys.ElementAt(listBox_Company.SelectedIndex);

                //Call the procedure to load the mysql data
                dataTable = DatabaseConnection.commandSelect("SELECT * FROM bedrijven WHERE id = " + row.Key + "");

                foreach (DataRow record in dataTable.Rows)
                {
                    textbox_bedrijf.Text = record["naam"].ToString();
                    textbox_bedrijfadres.Text = record["straat"].ToString() + " " + record["nummer"].ToString() + "" + record["toevoeging"].ToString();
                    textbox_bedrijfplaats.Text = record["plaats"].ToString();
                }
            }
        }

        private void listBox_Student_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_Student.SelectedIndex > -1)
            {
                var row = dicSearchStudents.ElementAt(listBox_Student.SelectedIndex);

                if ((bool)radioButton_Student1.IsChecked)
                {
                    textbox_student1.Text = row.Value;
                    textbox_student1nummer.Text = row.Key.ToString();
                }
                if ((bool)radioButton_Student2.IsChecked)
                {
                    if ((bool)checkbox_Afstudeer.IsChecked)
                    {
                        textbox_student2.Text = row.Value;
                        textbox_student2nummer.Text = row.Key.ToString();
                    }
                }
            }
        }

        private void button_subjectAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listbox_SubjectAll.SelectedItem != null)
            {
                DataRowView selection = (DataRowView)listbox_SubjectAll.SelectedItem;
                int id = Convert.ToInt32(selection.Row["id"]);

                DataTable tempTable = DatabaseConnection.commandSelect("CALL procedure_stage_kenmerken_add(" + stageId + "," + id + ");");
                getSubjectData();
            }
        }

        private void button_subjectDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listbox_SubjectChosen.SelectedItem != null)
            {
                DataRowView selection = (DataRowView)listbox_SubjectChosen.SelectedItem;
                int id = Convert.ToInt32(selection.Row["id"]);

                DataTable tempTable = DatabaseConnection.commandSelect("CALL procedure_stage_kenmerken_del(" + stageId + "," + id + ");");
                getSubjectData();
            }
        }

        //Method to save the trainee record
        public void updateTrainee()
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u de stage gegevens wilt aanpassen?", "Aanpassen", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DataRowView selection = (DataRowView)listBox_Company.SelectedItem;
                int bedrijf_id = ((listBox_Company.SelectedItem != null) ?
                    Convert.ToInt32(selection.Row["id"].ToString()) : bedrijfId);

                int studentNr2;
                if (textbox_student2nummer.Text.Equals(""))
                {
                    studentNr2 = 0;
                }
                else
                {
                    studentNr2 = Convert.ToInt32(textbox_student2nummer.Text);
                }

                dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_details_edit_update(" + stageId + "," + bedrijf_id + ",'" + textbox_bedrijfsbegeleider.Text + "','" + textbox_bedrijfsbegeleidertelefoon.Text + "','" + textbox_bedrijfsbegeleideremail.Text + "'," + Convert.ToInt32(textbox_student1nummer.Text) + "," + studentNr2 + "," + checkbox_toestemming.IsChecked + "," + checkbox_Afstudeer.IsChecked + "," + checkbox_goedkeuring.IsChecked + ",'" + textbox_opmerking.Text + "','" + textbox_opdracht.Text + "');");
            }
        }

        private void textbox_zoekstudent_KeyUp(object sender, KeyEventArgs e)
        {
            string search = textbox_zoekstudent.Text.Trim().ToLower();

            listBox_Student.Items.Clear();
            dicSearchStudents.Clear();
            foreach (var record in dicAllStudents)
            {
                if (record.Value.Trim().ToLower().Contains(search))
                {
                    dicSearchStudents.Add(record.Key, record.Value);
                    listBox_Student.Items.Add(record.Value);
                }
            }
        }

        private void textbox_zoekbedrijf_KeyUp(object sender, KeyEventArgs e)
        {
            string search = textbox_zoekbedrijf.Text.Trim().ToLower();

            listBox_Company.Items.Clear();
            dicSearchCompanys.Clear();
            foreach (var record in dicAllCompanys)
            {
                if (record.Value.Trim().ToLower().Contains(search))
                {
                    dicSearchCompanys.Add(record.Key, record.Value);
                    listBox_Company.Items.Add(record.Value);
                }
            }
        }
    }
}
