using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for TraineeList.xaml
    /// </summary>
    public partial class InstructorRatingList : UserControl
    {

        private MainWindow mainWindow;
        private static DataTable dataTable;
        private DataTable ids;
        private int TraineeID;
        private InstructorRatingListControl control;
        private string selectedInstructor;

        public string SelectedInstructor
        {
            get { return selectedInstructor; }
            set { selectedInstructor = value; }
        }


        public InstructorRatingList(MainWindow mainWindow, int TraineeID)
        {
            this.TraineeID = TraineeID;
            InitializeComponent();
            //this.control = control;

            this.mainWindow = mainWindow;

            // getData();

            // removeFirstColumn();

            //Set the datagrid context to the datatable
            // data.DataContext = dataTable;
        }

        //Call the procedure to load the mysql data
        private void getData()
        {
            List<Docent> list = new List<Docent>();
            Random random = new Random();
            list.Add(createDocent("Angelique van den Boogaard", random.Next(50, 700), 36, new string[3] { "Nederlands", "Engels", "Presentatievaardigheden" }));
            list.Add(createDocent("Ger Saris", random.Next(50, 700), 13, new string[3] { "C#", "C++", "Java" }));
            list.Add(createDocent("Bob Polis", random.Next(50, 700), 54, new string[3] { "XML", "GUI", "C#" }));
            list.Add(createDocent("Bob van der Putten", random.Next(50, 700), 8, new string[3] { "Game Design", "Software Architecture", "UML" }));
            list.Add(createDocent("Jasper van Rosmalen", random.Next(50, 700), 39, new string[3] { "Databases", "PHP", "MySQL" }));
            data.ItemsSource = list;

            // data.Columns.Add(new DataGridColumn());
            //  data.Columns.Add(new DataGridColumn("Hoi"));
            // data.Columns[1].DisplayIndex = 1;
            //data.Columns[2].DisplayIndex = 2;
            // data.Columns[0].Visibility = System.Windows.Visibility.Collapsed;
            dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_overzicht();");
        }

        private Docent createDocent(String naam, int rating, int afstand, string[] kenmerken)
        {
            Docent docent = new Docent();
            docent.Naam = naam;
            //docent.Afstand = afstand;
            docent.Rating = rating;

            foreach (string s in kenmerken)
            {
                docent.kenmerkenlijst.Add(s.Trim());
            }

            return docent;
        }

        private void data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }


        public void test(List<Docent> docList)
        {
            data.ItemsSource = docList;
        }

        public void setControl(InstructorRatingListControl control)
        {

            this.control = control;
        }

        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var myValue = (data.SelectedItem as Docent).Naam;
            control.changeContent("Geselecteerde docent: " + myValue);
            control.setDocentID((data.SelectedItem as Docent).Id);
        }

        private void data_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Id" || e.PropertyName == string.Empty)
            {
                e.Cancel = true;
            }

            if (e.Column.Header.ToString() == "Tijdvrij")
            {
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
    }
}
