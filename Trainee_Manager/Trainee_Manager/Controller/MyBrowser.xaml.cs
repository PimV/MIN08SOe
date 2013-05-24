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
using Trainee_Manager.View;
using System.Threading;
namespace Trainee_Manager.Controller
{
    /// <summary>
    /// Interaction logic for WebBrowser.xaml
    /// </summary>
    public partial class Browser : UserControl
    {

        public string Url;
        public int loadCount = 0;
        private Login2 login;

        public Boolean isNavigating;


        public Browser(Login2 login)
        {
            InitializeComponent();
            this.login = login;
        }

        public void PageLoaded(object sender, NavigationEventArgs e)
        {
            loadCount++;
            onLoad();
            isNavigating = false;

        }

        public void onLoad()
        {

            Url = myBrowser.Source.AbsoluteUri.ToString();

            if (!login.loggedIn)
            {
                login.tryStudentLogin();
                if (loadCount == 3)
                    login.Login();
            }
            // login.loggedIn = false;

            login.Url = Url;

            Console.WriteLine("Loaded");


        }

        private void myBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            isNavigating = true;
        }
    }
}
