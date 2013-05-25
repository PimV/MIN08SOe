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
    /// Interaction logic for InstructorsList.xaml
    /// </summary>
    public partial class InstructorsReport : UserControl
    {

        MainWindow mainWindow;
        private static DataTable dataTable;

        public InstructorsReport(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            getData();

            //Set the datagrid context to the datatable
            data.DataContext = dataTable;
        }

        //Call the procedure to load the mysql data
        private void getData()
        {
            dataTable = DatabaseConnection.commandSelect("CALL procedure_docent_overzicht();");
        }

        private void data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mainWindow.showInstructorDetails(getIdOfSelected());
        }

        public void deleteTeacher()
        {
            
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u de docent  wilt verwijderen?", "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int indexDelete = getIdOfSelected();
                //MessageBox.Show("" + piet);
                //DatabaseConnection.commandEdit("DELETE FROM docenten WHERE id = '"+ piet +"'");

                //DatabaseConnection.commandEdit("CALL procedure_docent_details_delete(" + indexDelete + ");");
                dataTable = DatabaseConnection.commandSelect("CALL procedure_docent_details_delete(" + indexDelete + ");");
                foreach (DataRow row in dataTable.Rows)
                {
                    MessageBoxResult result2 = MessageBox.Show(row["foutmelding"].ToString(), "Verwijderen mislukt", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                mainWindow.showInstructorsReport(); 
            }           
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
