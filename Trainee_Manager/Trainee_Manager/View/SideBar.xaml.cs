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
            ReportsSubMenu.SelectedIndex = 0;
        }

        public void setCoördinatorMode()
        {
            PreferencesButton.Visibility = Visibility.Collapsed;
            myStudentsButton.Visibility = Visibility.Collapsed;
        }

        public void setTeacherMode()
        {
            StageButton.Visibility = Visibility.Collapsed;
            ImportButton.Visibility = Visibility.Collapsed;
            /*ReportsCompaniesButton.Visibility = Visibility.Collapsed;
            ReportsInstructorsButton.Visibility = Visibility.Collapsed;
            ReportsStagesButton.Visibility = Visibility.Collapsed;*/
        }

        private void StageButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed();
            mainWindow.showTraineeScreen();
        }

        private void MainScreenButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed();
            mainWindow.showMainScreen();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed();
            toggleReportsSubMenu();
            showReport();
        }

        private void ReportsSubMenu_SelectionChanged(object sender, EventArgs e)
        {
            showReport();
        }

        /*private void ReportsStagesButton_Click(object sender, RoutedEventArgs e)
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
        }*/

        private void MyStudentsButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed();
            mainWindow.showMyStudents();
        }

        private void PreferencesButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed();
            mainWindow.showPreferencesScreen();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed();
            mainWindow.showLoginScreen();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed();
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

        private void showReport()
        {
            if (ReportsSubMenu.Text == "Stages")
            {
                mainWindow.showStagesReport();
            }
            else if (ReportsSubMenu.Text == "Docenten")
            {
                mainWindow.showInstructorsReport();
            }
            else if (ReportsSubMenu.Text == "Bedrijven")
            {
                mainWindow.showCompaniesReport();
            }
        }

        private void buttonPressed()
        {
            ReportsSubMenu.Visibility = Visibility.Collapsed;
        }

    }
}
