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
    public partial class TraineeReportControl : UserControl
    {

        private MainWindow mainWindow;
        private TraineeReport contentPage;
        private static DataTable dataTable;

        public TraineeReportControl(MainWindow mainWindow, TraineeReport contentPage)
        {
            // TODO: Complete member initialization
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

        private void buttonFilter_Click(object sender, RoutedEventArgs e)
        {
            string begeleider = "0";

            if ((bool)radio_Begeleider.IsChecked)
            {
                begeleider = "1";
            }
            else if ((bool)radio_ZonderBegeleider.IsChecked)
            {
                begeleider = "-1";
            }

            string periode = ((ComboBoxItem)comboBox_Priode.SelectedItem).Tag.ToString();

            string zoek = textBox_Zoekterm.Text;

            contentPage.getData(begeleider, periode, zoek);
        }

        private void printButton_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonVerwijder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
