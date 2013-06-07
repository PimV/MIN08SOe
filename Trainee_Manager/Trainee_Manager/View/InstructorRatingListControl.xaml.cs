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
using Trainee_Manager.Controller;
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
        private int docID;
        private KoppelController _ratingController;
        public InstructorRatingList contentPage;
        //private string selectedInstructor ;

        //public string SelectedInstructor
        //{
        //    get { return selectedInstructor; }
        //    set { selectedInstructor = value; }
        //}

        public InstructorRatingListControl(MainWindow mainWindow, int TraineeID, InstructorRatingList contentPage, KoppelController _ratingController)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            this._ratingController = _ratingController;
            this.TraineeID = TraineeID;
            this.contentPage = contentPage;
           // selectedInstructorLabel.DataContext = SelectedInstructor;
        }

        public void changeContent(string contentChange)
        {
            selectedInstructorLabel.Content = contentChange;
        }

        public void setDocentID(int id)
        {
            this.docID = id;
        }

        private void koppelDocent(object sender, RoutedEventArgs e)
        {
            _ratingController.KoppelDocent(docID, TraineeID);   
        }
    }
}
