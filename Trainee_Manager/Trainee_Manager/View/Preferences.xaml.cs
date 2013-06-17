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
    /// Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : UserControl
    {
        private int docentId;
        private static DataTable dataTable;

        Dictionary<int, string> dicAllSubjects, dicChoosenSubjects, dicAllTrainees, dicChoosenTrainees, dicAllCompanys, dicChoosenCompanys;

        public Preferences(int id)
        {
            InitializeComponent();

            docentId = id;

            createDictionarys();
        }

        private void createDictionarys()
        {
            dicAllSubjects = new Dictionary<int, string>();
            dicChoosenSubjects = new Dictionary<int, string>();

            dicAllTrainees = new Dictionary<int, string>();
            dicChoosenTrainees = new Dictionary<int, string>();

            dicAllCompanys = new Dictionary<int, string>();
            dicChoosenCompanys = new Dictionary<int, string>();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (tabSubjects.IsSelected)
                {
                    tabSubjectsOpen();
                }
                if (tabCompanys.IsSelected)
                {
                    tabCompanysOpen();
                }
                if (tabTrainees.IsSelected)
                {
                    tabTraineesOpen();
                }
            }
        }

        private void tabSubjectsOpen()
        {
            //Clear both the listboxes to fill them again
            listboxAllSubjects.Items.Clear();
            listboxChoosenSubjects.Items.Clear();

            //Clear both the dictionarys to fillt hem again
            dicAllSubjects.Clear();
            dicChoosenSubjects.Clear();

            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_kenmerken_not_docent(" + docentId + ");");
            
            foreach (DataRow row in dataTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string text = row["naam"].ToString();

                //Add the text and id to the dictionary for later use
                dicAllSubjects.Add(id, text);

                listboxAllSubjects.Items.Add(text);
            }


            //Call the procedure to load the subjects linked to the instructor
            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_kenmerken_docent(" + docentId + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string text = row["naam"].ToString();

                //Add the text and id to the dictionary for later use
                dicChoosenSubjects.Add(id, text);

                listboxChoosenSubjects.Items.Add(text);
            }
        }

        private void tabCompanysOpen()
        {
            //Clear both the listboxes to fill them again
            listboxAllCompanys.Items.Clear();
            listboxChoosenCompanys.Items.Clear();

            //Clear both the dictionarys to fillt hem again
            dicAllCompanys.Clear();
            dicChoosenCompanys.Clear();

            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_bedrijf_not_docent(" + docentId + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string text = row["naam"].ToString();

                //Add the text and id to the dictionary for later use
                dicAllCompanys.Add(id, text);

                listboxAllCompanys.Items.Add(text);
            }


            //Call the procedure to load the subjects linked to the instructor
            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_bedrijf_docent(" + docentId + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string text = row["naam"].ToString();

                //Add the text and id to the dictionary for later use
                dicChoosenCompanys.Add(id, text);

                listboxChoosenCompanys.Items.Add(text);
            }
        }

        private void tabTraineesOpen()
        {
            //Clear both the listboxes to fill them agains
            listboxAllTrainees.Items.Clear();
            listboxChoosenTrainees.Items.Clear();

            //Clear both the dictionarys to fillt hem again
            dicAllTrainees.Clear();
            dicChoosenTrainees.Clear();

            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_stage_not_docent(" + docentId + ");");

            int temp = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string text = row["naam"].ToString();
                Console.WriteLine(temp + " " + text + " " + id);
                //Add the text and id to the dictionary for later use
                dicAllTrainees.Add(id,text);
                Console.WriteLine(temp + " " + text + " " + id);

                listboxAllTrainees.Items.Add(text);
                temp++;
            }


            //Call the procedure to load the subjects linked to the instructor
            dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_stage_docent(" + docentId + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                string text = row["naam"].ToString();

                //Add the text and id to the dictionary for later use
                dicChoosenTrainees.Add(id, text);

                listboxChoosenTrainees.Items.Add(text);
            }
        }

        //Add subject to instructors preferences
        private void buttonSubjectAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listboxAllSubjects.SelectedItem != null)
            {
                int id = dicAllSubjects.ElementAt(listboxAllSubjects.SelectedIndex).Key;

                dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_kenmerken_add(" + docentId + ", " + id + ");");
            }

            //Reload the lixtboxes
            tabSubjectsOpen();
        }

        //Delete subject from instructors preferences
        private void buttonSubjectDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listboxChoosenSubjects.SelectedItem != null)
            {
                int id = dicChoosenSubjects.ElementAt(listboxChoosenSubjects.SelectedIndex).Key;

                dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_kenmerken_del(" + docentId + ", " + id + ");");
            }

            //Reload the lixtboxes
            tabSubjectsOpen();
        }

        //Add company to instructors preferences
        private void buttonCompanyAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listboxAllCompanys.SelectedItem != null)
            {
                int id = dicAllCompanys.ElementAt(listboxAllCompanys.SelectedIndex).Key;

                dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_bedrijf_add(" + docentId + ", " + id + ");");
            }

            //Reload the lixtboxes
            tabCompanysOpen();
        }

        //Delete company from instructors preferences
        private void buttonCompanyDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listboxChoosenCompanys.SelectedItem != null)
            {
                int id = dicChoosenCompanys.ElementAt(listboxChoosenCompanys.SelectedIndex).Key;

                dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_bedrijf_del(" + docentId + ", " + id + ");");
            }

            //Reload the lixtboxes
            tabCompanysOpen();
        }

        //Add trainee to instructors preferences
        private void buttonTraineeAdd_Click(object sender, RoutedEventArgs e)
        {
            if (listboxAllTrainees.SelectedItem != null)
            {
                int id = dicAllTrainees.ElementAt(listboxAllTrainees.SelectedIndex).Key;

                dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_stage_add(" + docentId + ", " + id + ");");
            }

            //Reload the lixtboxes
            tabTraineesOpen();
        }

        //Delete trainee for instructors preferences
        private void buttonTraineeDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listboxChoosenTrainees.SelectedItem != null)
            {
                int id = dicChoosenTrainees.ElementAt(listboxChoosenTrainees.SelectedIndex).Key;

                dataTable = DatabaseConnection.commandSelect("CALL procedure_voorkeuren_stage_del(" + docentId + ", " + id + ");");
            }

            //Reload the lixtboxes
            tabTraineesOpen();
        }
    }
}
