using System;
using System.Collections.Generic;
using System.Data;
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

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for TraineeReportControl.xaml
    /// </summary>
    public partial class InstructorTraineeReportControl : UserControl
    {
        private static DataTable dataTable;
        private MainWindow mainWindow;
        private InstructorTraineeReport contentPage;

        public InstructorTraineeReportControl(MainWindow mainWindow, InstructorTraineeReport contentPage)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.contentPage = contentPage;

            fillDropDownPeriod();
        }

        private void fillDropDownPeriod()
        {
            dataTable = DatabaseConnection.commandSelect("SELECT id, periode FROM periodes ORDER BY id ASC");
            foreach (DataRow row in dataTable.Rows)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = row["periode"];
                comboBoxItem.Tag = row["id"];
                comboBox_Priode.Items.Add(comboBoxItem);
            }

            comboBox_Priode.SelectedIndex = comboBox_Priode.Items.Count - 1;
        }

        private void printButton_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void comboBox_Priode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string periode = ((ComboBoxItem)comboBox_Priode.SelectedItem).Tag.ToString();
            contentPage.getData(periode);
        }
    }
}
