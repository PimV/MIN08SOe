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
    /// Interaction logic for InstructorDetailsControl.xaml
    /// </summary>
    public partial class InstructorDetailsControl : UserControl
    {

        private MainWindow mainwindow;
        private View.InstructorDetails contentPage;


        public InstructorDetailsControl(MainWindow mainwindow, View.InstructorDetails contentPage)
        {
            InitializeComponent();

            this.mainwindow = mainwindow;
            this.contentPage = contentPage;

            if (mainwindow.InstructorId > 0)
            {
                buttonAnnuleren.Visibility = Visibility.Collapsed;
                buttonOpslaan.Margin = new Thickness(0,0,10,10);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            mainwindow.showInstructorsReport();
        }
            
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            contentPage.updateInstructor();
        }
    }
}
