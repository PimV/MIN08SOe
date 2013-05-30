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
        private Button selectedButton;

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
            wrapPanel_Reports.Visibility = Visibility.Collapsed;
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

        private void selectButton(Button button)
        {
            if (selectedButton != null)
            {
                selectedButton.ClearValue(Button.BackgroundProperty);
            }

            BrushConverter bc2 = new BrushConverter();
            Brush LightBlueBrush = (Brush)bc2.ConvertFrom("#FFBEE6FD");
            button.Background = LightBlueBrush;
            selectedButton = button;
        }

        private void openReportsSubMenu()
        {
            wrapPanel_Reports.Visibility = Visibility.Visible;
        }

        private void closeReportsSubmenu()
        {
            wrapPanel_Reports.Visibility = Visibility.Collapsed;
        }

        private void StageButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showTraineeScreen();
            closeReportsSubmenu();
            selectButton((Button)sender);
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            if (wrapPanel_Reports.Visibility == Visibility.Collapsed)
            {
                openReportsSubMenu();
            }
            else
            {
                closeReportsSubmenu();
            }
        }

        private void StagesButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showStagesReport();
            selectButton((Button)sender);
        }

        private void DocentenButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showInstructorsReport();
            selectButton((Button)sender);
        }

        private void BedrijvenButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showCompaniesReport();
            selectButton((Button)sender);
        }

        private void StudentenButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showStudentsReport();
            selectButton((Button)sender);
        }

        private void MyStudentsButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showMyStudents();
            selectButton((Button)sender);
        }

        private void PreferencesButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showPreferencesScreen(mainWindow.InstructorId);
            selectButton((Button)sender);
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showImportScreen();
            closeReportsSubmenu();
            selectButton((Button)sender);
        }

        private void InstructorMyInfoButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showInstructorDetails(mainWindow.InstructorId);
            selectButton((Button)sender);
        }

        private void beheerButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showBeheerScreen();
            closeReportsSubmenu();
            selectButton((Button)sender);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showLoginScreen();
            
        }

    }
}
