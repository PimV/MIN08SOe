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
    /// Interaction logic for TraineeList.xaml
    /// </summary>
    public partial class TraineeList : UserControl
    {

        private MainWindow mainWindow;
        private static DataTable dataTable;

        public TraineeList(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            //Call the procedure to load the mysql data
            dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_lijst();");

            //Set the datagrid context to the datatable
            data.DataContext = dataTable;
        }

        private void data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int rowNumber = data.SelectedIndex;
            TextBlock block = data.Columns[0].GetCellContent(data.Items[rowNumber]) as TextBlock;

            int id = Convert.ToInt32(block.Text);

            mainWindow.showTraineeDetailsScreen(id);
        }
    }
}
