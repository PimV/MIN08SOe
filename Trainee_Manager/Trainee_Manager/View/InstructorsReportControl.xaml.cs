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
    /// Interaction logic for InstructorsReportControl.xaml
    /// </summary>
    public partial class InstructorsReportControl : UserControl
    {

        private MainWindow mainWindow;
        private View.InstructorsReport contentPage;

        public InstructorsReportControl(MainWindow mainWindow, View.InstructorsReport contentPage)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.contentPage = contentPage;
        }

        private void printButton_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showInstructorDetails(-1);
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            contentPage.deleteTeacher();
        }
    }
}
