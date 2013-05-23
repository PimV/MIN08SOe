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
    /// Interaction logic for SideBar.xaml
    /// </summary>
    public partial class SideBar : UserControl
    {

        private MainWindow mainWindow;

        public SideBar(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            StageButton.Visibility = Visibility.Collapsed;
            ReportsButton.Visibility = Visibility.Collapsed;
            myStudentsButton.Visibility = Visibility.Collapsed;
            PreferencesButton.Visibility = Visibility.Collapsed;
            ImportButton.Visibility = Visibility.Collapsed;
            beheerButton.Visibility = Visibility.Collapsed;
            instructorMyInfoButton.Visibility = Visibility.Collapsed;
        }

        public void setCoördinatorMode()
        {
            StageButton.Visibility = Visibility.Visible;
            ReportsButton.Visibility = Visibility.Visible;
            ImportButton.Visibility = Visibility.Visible;
            beheerButton.Visibility = Visibility.Visible;
        }

        public void setTeacherMode()
        {
            myStudentsButton.Visibility = Visibility.Visible;
            PreferencesButton.Visibility = Visibility.Visible;
            instructorMyInfoButton.Visibility = Visibility.Visible;
        }

        public void setStudentMode()
        {

        }

        private void StageButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showTraineeScreen();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showStagesReport();
        }

        private void MyStudentsButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showMyStudents();
        }

        private void PreferencesButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showPreferencesScreen();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showLoginScreen();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showImportScreen();
        }

        private void InstructorMyInfoButton_Click(object sender, RoutedEventArgs e)
        {
            //int id is een tijdelijke NEP int, deze id moet de unique id van de docent in de docenten table gaan bevatten
            MessageBox.Show("Let op, waarschijnlijk is dit niet de gegevens van juiste docent, check broncode waarom");
            int id = 1;
            mainWindow.showInstructorDetails(id);
        }

        private void beheerButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showBeheerScreen();
        }

    }
}
