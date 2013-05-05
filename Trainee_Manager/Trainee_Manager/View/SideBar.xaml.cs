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

            ReportsSubMenu.Visibility = Visibility.Collapsed;
        }

        public void setCoördinatorMode()
        {
            PreferencesButton.Visibility = Visibility.Collapsed;
            ReportsMyStudentsButton .Visibility = Visibility.Collapsed;
        }

        public void setTeacherMode()
        {
            StageButton.Visibility = Visibility.Collapsed;
            ImportButton.Visibility = Visibility.Collapsed;
            ReportsCompaniesButton.Visibility = Visibility.Collapsed;
            ReportsInstructorsButton.Visibility = Visibility.Collapsed;
            ReportsStagesButton.Visibility = Visibility.Collapsed;
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
            toggleReportsSubMenu();
        }

        private void ReportsMyStudentsButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showMyStudentsReport();
        }

        private void ReportsStagesButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showStagesReport();
        }

        private void ReportsInstructorsButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showInstructorsReport();
        }

        private void ReportsCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showCompaniesReport();
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

        private void toggleReportsSubMenu()
        {
            if (ReportsSubMenu.Visibility == Visibility.Collapsed)
            {
                ReportsSubMenu.Visibility = Visibility.Visible;
            }
            else
            {
                ReportsSubMenu.Visibility = Visibility.Collapsed;
            }
        }

    }
}
