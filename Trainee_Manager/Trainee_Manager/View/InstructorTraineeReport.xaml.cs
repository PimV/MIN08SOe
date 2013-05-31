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
    /// Interaction logic for TraineeReport.xaml
    /// </summary>
    public partial class InstructorTraineeReport : UserControl
    {
        private MainWindow mainWindow;
        private static DataTable dataTable;

        //Unique email of an instructor
        private string email;

        private int docentId;

        public InstructorTraineeReport(MainWindow mainWindow, string email)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            //Deze code hebben we tijdelijk nodig, voor het geval we met onze FAKE DOCENT account inloggen
           // if (email.Equals(""))
           // {
               // this.email = "gbj.saris@avans.nl";
          //  }
           // else
           // {
                this.email = email;
           // }

            //Get the correct instructor
            getInstructor();

            //Show the instructors trainees
            getData();

            //Set the datagrid context to the datatable
            data.DataContext = dataTable;

            //Set the instructor id if needed on the preference page or my data page
            mainWindow.InstructorId = docentId;
        }

        private void getInstructor()
        {
            dataTable = DatabaseConnection.commandSelect("CALL procedure_docent_get_id('" + email + "');");
            //dataTable = DatabaseConnection.commandSelect("SELECT id FROM docenten WHERE postcode = '" + email + "'");

            foreach (DataRow row in dataTable.Rows)
            {
                docentId = Convert.ToInt32(row["id"]);
            }
        }

        //Call the procedure to load the mysql data
        private void getData()
        {
            dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_overzicht_vandocent(" + docentId + ");");
        }

        private void data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mainWindow.showTraineeDetailsScreenViaInstructor(getIdOfSelected());
        }

        private int getIdOfSelected()
        {
            int rowNumber = data.SelectedIndex;
            TextBlock block = data.Columns[0].GetCellContent(data.Items[rowNumber]) as TextBlock;

            int id = Convert.ToInt32(block.Text);

            return id;
        }
    }
}
