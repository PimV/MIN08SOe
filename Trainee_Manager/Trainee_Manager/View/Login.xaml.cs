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

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {

        private Model.Session sessionModel;
        private MainWindow mainWindow;

        public Login(Model.Session sessionModel, MainWindow mainWindow)
        {
            InitializeComponent();

            this.sessionModel = sessionModel;
            this.mainWindow = mainWindow;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            sessionModel.UserName = userName_TextBox.Text;
            sessionModel.Function = function_DropDown.Text;
            mainWindow.showSideBar();
            mainWindow.showMainScreen();
        }
    }
}
