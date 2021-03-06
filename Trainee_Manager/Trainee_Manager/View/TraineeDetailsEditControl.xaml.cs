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

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for TraineeDetailsEditControl.xaml
    /// </summary>
    public partial class TraineeDetailsEditControl : UserControl
    {

        private MainWindow mainWindow;
        private TraineeDetailsEdit contentPage;

        public TraineeDetailsEditControl(MainWindow mainWindow, TraineeDetailsEdit contentPage)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.contentPage = contentPage;
        }

        private void button_annuleren_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.showTraineeDetailsScreen();
        }

        private void button_opslaan_Click(object sender, RoutedEventArgs e)
        {
            contentPage.updateTrainee();
            mainWindow.showTraineeDetailsScreen();
        }
    }
}
