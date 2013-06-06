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
using Trainee_Manager.Model;

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for TraineeDetailsControl.xaml
    /// </summary>
    public partial class InstructorRatingListControl : UserControl
    {

        MainWindow mainWindow;
        private int TraineeID;
        public InstructorRatingList contentPage;
        //private string selectedInstructor ;

        //public string SelectedInstructor
        //{
        //    get { return selectedInstructor; }
        //    set { selectedInstructor = value; }
        //}

        public InstructorRatingListControl(MainWindow mainWindow, int TraineeID, InstructorRatingList contentPage)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            this.TraineeID = TraineeID;
            this.contentPage = contentPage;
           // selectedInstructorLabel.DataContext = SelectedInstructor;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showTraineeEditScreen();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainWindow.showPossibleInstructors();
        }

        public void changeContent(string contentChange)
        {
            selectedInstructorLabel.Content = contentChange;
        }
    }
}
