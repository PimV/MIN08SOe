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

            MainScreenButton.Visibility = Visibility.Collapsed;
            StageButton.Visibility = Visibility.Collapsed;
            ReportsButton.Visibility = Visibility.Collapsed;
            myStudentsButton.Visibility = Visibility.Collapsed;
            PreferencesButton.Visibility = Visibility.Collapsed;
            ImportButton.Visibility = Visibility.Collapsed;
        }

        public void setCoördinatorMode()
        {
            MainScreenButton.Visibility = Visibility.Visible;
            StageButton.Visibility = Visibility.Visible;
            ReportsButton.Visibility = Visibility.Visible;
            ImportButton.Visibility = Visibility.Visible;
        }

        public void setTeacherMode()
        {
            MainScreenButton.Visibility = Visibility.Visible;
            myStudentsButton.Visibility = Visibility.Visible;
            PreferencesButton.Visibility = Visibility.Visible;
        }

        public void setStudentMode()
        {

        }

        private void StageButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showTraineeScreen();
        }

        private void MainScreenButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showMainScreen();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showReportControl();
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

    }
}
