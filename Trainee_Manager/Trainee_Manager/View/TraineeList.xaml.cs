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
        private static DataTable dt;

        public TraineeList(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            //dt = DatabaseConnection.commandSelect("SELECT student_id AS StudentNr, (SELECT CONCAT(roepnaam, ' ', achternaam) FROM studenten WHERE id = student_id) AS Student, afstudeerder AS Afstudeerstage, bedrijf_id AS Bedrijf, docent_id AS Begeleider FROM stages");
           // dt = DatabaseConnection.commandSelect("CALL test_procedure();");
            getData();
            
            data.DataContext = dt;
        }

        private void PairTeachers_Button(object sender, RoutedEventArgs e)
        {
            mainWindow.showTraineeDetailsScreen();
        }

        private void data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int i = data.SelectedIndex;


            TextBlock block = data.Columns[0].GetCellContent(data.Items[i]) as TextBlock;




            Console.WriteLine(block.Text);
            Console.WriteLine("Index: " + i);
           // mainWindow.showTraineeDetailsScreen();
        }

        private void getData()
        {
            dt = DatabaseConnection.commandSelect("CALL test_procedure();");
        }
    }
}
