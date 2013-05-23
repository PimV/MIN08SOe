using System;
using System.Collections.Generic;
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

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for TraineeDetailsEditControl.xaml
    /// </summary>
    public partial class TraineeDetailsEditControl : UserControl
    {

        MainWindow mainWindow;

        public TraineeDetailsEditControl(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            //Tijdelijk fake int gemaakt omdat methode die verwacht
            int id = 1;

            mainWindow.showTraineeDetailsScreen(id);
        }
    }
}
