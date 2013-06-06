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

//TODO: Benodigde uren voor verschillende stages kunnen opgeven. 
//TODO: Punten opgeven voor factoren van het berekenen van docenten. 
namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for SubjectDetails.xaml
    /// </summary>
    public partial class ConfigPage : UserControl
    {
        public ConfigPage()
        {
            InitializeComponent();
            loadSubjects();
        }

        private void loadSubjects()
        {
            //Fill the listbox containing ALL subjects
            DataTable tempTable = DatabaseConnection.commandSelect("SELECT * FROM kenmerken");
            listbox_SubjectAll.SelectedValuePath = "id";
            listbox_SubjectAll.DisplayMemberPath = "kenmerk";
            listbox_SubjectAll.ItemsSource = tempTable.DefaultView;
        }

        private void button_SubjectNew_Click(object sender, RoutedEventArgs e)
        {
            if (newSubjectText.Text.Trim() != "")
            {
                DatabaseConnection.commandEdit("INSERT INTO kenmerken (kenmerk) SELECT * FROM (SELECT '" + newSubjectText.Text + "') AS tmp WHERE NOT EXISTS (SELECT * FROM kenmerken WHERE kenmerk = '" + newSubjectText.Text + "');");
                loadSubjects();
            }
            newSubjectText.Clear();
        }

        private void button_subjectDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listbox_SubjectAll.SelectedItem != null)
            {
                DataRowView selection = (DataRowView)listbox_SubjectAll.SelectedItem;
                int id = Convert.ToInt32(selection.Row["id"]);


                DataTable tempTable = DatabaseConnection.commandSelect("CALL procedure_kenmerken_delete(" + id + ");");
                loadSubjects();
            }
        }
    }
}
