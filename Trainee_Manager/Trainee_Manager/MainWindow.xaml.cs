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

namespace Trainee_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private View.SideBar sideBar;
        private UserControl currentTopArea;
        private UserControl currentContentArea;

        public Model.Session currentSession { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            currentSession = new Model.Session();
            showLoginScreen();
        }

        public void showSideBar()
        {

            sideBar = new View.SideBar(this);
            if (currentSession.Function == "Coördinator")
            {
                sideBar.setCoördinatorMode();
            }
            else
            {
                sideBar.setTeacherMode();
            }

            sideBarArea.Child = sideBar;
        }

        public void showTraineeScreen()
        {
            clearTopAndContentAreas();
            currentTopArea = new View.TraineeListFilters();
            topArea.Child = currentTopArea;
            currentContentArea = new View.TraineeList(this);
            contentArea.Child = currentContentArea;
        }

        public void showTraineeDetailsScreen()
        {
            clearTopAndContentAreas();
            currentContentArea = new View.TraineeDetails();
            contentArea.Child = currentContentArea;
        }

        public void showMainScreen()
        {
            clearTopAndContentAreas();
        }

        public void showReportsScreen()
        {
            
        }

        public void showPreferencesScreen()
        {
            
        }

        public void showImportScreen()
        {
            
        }

        public void showLoginScreen()
        {
            clearTopAndContentAreas();
            clearSideBar();
            currentContentArea = new View.Login(currentSession, this);
            contentArea.Child = currentContentArea;
        }

        private void clearTopAndContentAreas()
        {
            if (currentTopArea != null)
            {
                currentTopArea = null;
                topArea.Child = null;
            }

            if (currentContentArea != null)
            {
                currentContentArea = null;
                contentArea.Child = null;
            }
        }

        private void clearSideBar()
        {
            if (sideBar != null)
            {
                sideBar = null;
                sideBarArea.Child = null;
            }
        }

        private void AvansLogo_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(currentSession.LoggedIn)
            {
                showMainScreen();
            }
        }

    }
}
