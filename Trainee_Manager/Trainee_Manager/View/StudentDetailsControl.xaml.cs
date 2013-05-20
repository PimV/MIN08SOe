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
    /// Interaction logic for StudentDetailsControl.xaml
    /// </summary>
    public partial class StudentDetailsControl : UserControl
    {
        private MainWindow mainWindow;

        public StudentDetailsControl(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showStudentsReport();
        }
    }
}
