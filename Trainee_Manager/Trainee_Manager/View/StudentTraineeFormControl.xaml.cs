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
using Trainee_Manager.Model;

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for StudentTraineeFormControl.xaml
    /// </summary>
    public partial class StudentTraineeFormControl : UserControl
    {

        private StudentTraineeForm studentTraineeForm;
        private DataTable dataTable;
        private int periodeId;

        public StudentTraineeFormControl(StudentTraineeForm studentTraineeForm)
        {
            InitializeComponent();
            this.studentTraineeForm = studentTraineeForm;
            getPeriods();
        }

        //StudentTraineeForm laten updaten naar de stage uit de geselecteerde periode. 
        private void comboBoxPeriod_Closed(object sender, EventArgs e)
        {
            updateContentArea();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            studentTraineeForm.save();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            updateContentArea();
        }

        //De periodes ophalen waar de student stages in geregistreerd heeft. In de dropdown plaatsen. 
        private void getPeriods()
        {
            ComboBoxItem comboBoxItem;
            dataTable = DatabaseConnection.commandSelect("SELECT " +
                                                         "per.id, " +
                                                         "per.periode " +
                                                         "FROM " +
                                                         "studenten_periodes AS sp " +
                                                         "LEFT JOIN periodes AS per ON sp.periode_id = per.id " +
                                                         "LEFT JOIN studenten AS stu ON sp.student_id = stu.id " +
                                                         "WHERE " +
                                                         "stu.studentnr = " + Session.ID);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = row["periode"];
                comboBoxItem.Tag = row["id"];
                comboBox_Period.Items.Add(comboBoxItem);
            }
            comboBox_Period.SelectedIndex = comboBox_Period.Items.Count - 1;

            if (comboBox_Period.Items.Count >= 1)
            {
                updateContentArea();
            }
        }

        private void updateContentArea()
        {
            periodeId = Int32.Parse(((ComboBoxItem)comboBox_Period.SelectedItem).Tag.ToString());
            studentTraineeForm.showPeriod(periodeId);            
        }
    }
}
