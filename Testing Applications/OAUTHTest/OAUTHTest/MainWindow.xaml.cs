using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OAUTHTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Manager auth = new Manager();
            acquire();
            test();
           // myBrowser.Navigate(new Uri("https://publicapi.avans.nl/oauth/saml.php"));
        }

        private void acquire()
        {
            var manager = new Manager();
            OAuthResponse reqToken = manager.AcquireRequestToken("https://publicapi.avans.nl/oauth/request_token", "POST");
            Console.WriteLine(reqToken.AllText);
            string url = "https://publicapi.avans.nl/oauth/saml.php?" + reqToken.AllText;
            myBrowser.Navigate(new Uri(url));
        }

        private void test()
        {
            // InitializeComponent();

        }
    }
}
