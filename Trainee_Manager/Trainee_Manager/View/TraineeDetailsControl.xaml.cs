﻿using System;
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
    public partial class TraineeDetailsControl : UserControl
    {

        MainWindow mainWindow;
        private int TraineeID;
        public RatingGegevensImporter ratingCalc;

        public TraineeDetailsControl(MainWindow mainWindow, int TraineeID, RatingGegevensImporter r)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            this.TraineeID = TraineeID;
            this.ratingCalc = r;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showTraineeEditScreen();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainWindow.showPossibleInstructors();
        }

        private void ontkoppelDocent(object sender, RoutedEventArgs e)
        {
            DatabaseConnection.commandEdit("CALL procedure_ontkoppel_docent(" + TraineeID + ")");
        }
    }
}
