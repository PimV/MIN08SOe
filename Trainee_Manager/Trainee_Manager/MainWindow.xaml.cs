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
        private UserControl currentControlArea;
        private UserControl currentContentArea;

        public Model.Session currentSession { get; set; }

        public MainWindow(Model.Session sessionModel)
        {
            InitializeComponent();

            currentSession = sessionModel;
            showSideBar();
            showMainScreen();
        }

        public void showSideBar()
        {

            sideBar = new View.SideBar(this);
            if (currentSession.Function == "Coördinator")
            {
                sideBar.setCoördinatorMode();
            }
            else if (currentSession.Function == "Docent")
            {
                sideBar.setTeacherMode();
            }
            else if (currentSession.Function == "Student")
            {
                sideBar.setStudentMode();
            }

            sideBarArea.Child = sideBar;
        }

        public void showTraineeScreen()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.TraineeListFilters();
            controlArea.Child = currentControlArea;
            currentContentArea = new View.TraineeList(this);
            contentArea.Child = currentContentArea;
        }

        public void showTraineeDetailsScreen()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.TraineeDetailsControl(this);
            controlArea.Child = currentControlArea;
            currentContentArea = new View.TraineeDetails();
            contentArea.Child = currentContentArea;
        }

        public void showTraineeEditScreen()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.TraineeDetailsEditControl(this);
            controlArea.Child = currentControlArea;
            currentContentArea = new View.TraineeDetailsEdit();
            contentArea.Child = currentContentArea;
        }

        public void showMainScreen()
        {
            if (currentSession.Function == "Coördinator")
            {
                showTraineeScreen();
            }
            else if (currentSession.Function == "Docent")
            {
                showMyStudents();
            }
            else if (currentSession.Function == "Student")
            {
                showStudentsTraineeForm();
            }
        }

        public void showStudentsTraineeForm()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.StudentTraineeFormControl();
            controlArea.Child = currentControlArea;
            currentContentArea = new View.StudentTraineeForm();
            contentArea.Child = currentContentArea;
        }

        public void showMyStudents()
        {
            clearTopAndContentAreas();
            //TODO: Nieuwe userControl voor aanmaken. 
            currentControlArea = new View.TraineeReportControl(this);
            controlArea.Child = currentControlArea;
            //TODO: Aparte userControl? Of kan dezelfde gewoon gebruikt worden?
            currentContentArea = new View.TraineeReport();
            contentArea.Child = currentContentArea;

            ((View.TraineeReportControl)currentControlArea).teacherMode();
        }

        public void showStagesReport()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.TraineeReportControl(this);
            controlArea.Child = currentControlArea;
            currentContentArea = new View.TraineeReport();
            contentArea.Child = currentContentArea;
        }

        public void showCompaniesReport()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.CompanyReportControl(this);
            controlArea.Child = currentControlArea;
            currentContentArea = new View.CompanyReport(this);
            contentArea.Child = currentContentArea;
        }

        public void showInstructorsReport()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.InstructorsReportControl(this);
            controlArea.Child = currentControlArea;
            currentContentArea = new View.InstructorsReport(this);
            contentArea.Child = currentContentArea;
        }

        public void showStudentsReport()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.StudentReportControl(this);
            controlArea.Child = currentControlArea;
            currentContentArea = new View.StudentReport(this);
            contentArea.Child = currentContentArea;
        }

        public void showStudentDetails()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.StudentDetailsControl(this);
            controlArea.Child = currentControlArea;
            currentContentArea = new View.StudentDetails();
            contentArea.Child = currentContentArea;
        }

        public void showPreferencesScreen()
        {
            clearTopAndContentAreas();
            currentContentArea = new View.Preferences();
            contentArea.Child = currentContentArea;
        }

        public void showImportScreen()
        {
            clearTopAndContentAreas();
            currentContentArea = new View.Import();
            contentArea.Child = currentContentArea;
        }

        public void showLoginScreen()
        {
            View.Login2 login = new View.Login2();
            login.Visibility = Visibility.Visible;
            this.Close();
        }

        public void showBeheerScreen()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.ConfigPageControl();
            controlArea.Child = currentControlArea;
            currentContentArea = new View.ConfigPage();
            contentArea.Child = currentContentArea;
        }

        public void showCompanyDetails()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.CompanyDetailsControl(this);
            controlArea.Child = currentControlArea;
            currentContentArea = new View.CompanyDetails();
            contentArea.Child = currentContentArea;
        }

        public void showInstructorDetails()
        {
            clearTopAndContentAreas();
            currentControlArea = new View.InstructorDetailsControl(this);
            controlArea.Child = currentControlArea;
            currentContentArea = new View.InstructorDetails();
            contentArea.Child = currentContentArea;
        }

        private void clearTopAndContentAreas()
        {
            if (currentControlArea != null)
            {
                currentControlArea = null;
                controlArea.Child = null;
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
