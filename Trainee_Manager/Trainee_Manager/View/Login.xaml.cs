using Helper_Classes.Trainee_Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Xml;
using Trainee_Manager.Controller;

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for Login2.xaml
    /// </summary>
    public partial class Login2 : Window
    {

        private string _url;
        public string Url
        {
            get
            {
                return browser.myBrowser.Source.AbsoluteUri.ToString();
            }
            set
            {
                value = _url;
            }
        }

        private Manager manager;
        private string _pin;
        private string login = "";

        private bool _loggedIn;
        public bool loggedIn
        {
            get
            {
                return _loggedIn;
            }
            set
            {
                _loggedIn = value; ;
            }
        }

        private string username = "";
        private string password = "";
        private string function = "";

        private string baseUrl;

        private Browser browser;

        private Model.Session sessionModel;

        //  public delegate void LoadedHandler();
        //  public event LoadedHandler loaded;

        //private static Controller.DatabaseConnection connection;
        private  Controller.MD5Encrypter encrypter;

        public Login2()
        {
            InitializeComponent();

            userName_TextBox.Focus();

            manager = new Manager();
            browser = new Browser(this);

           // connection = new Controller.DatabaseConnection();
            encrypter = new Controller.MD5Encrypter();

            //Set the connection string correct (using the app.config)
            DatabaseConnection.initializeConnection();

            browserGrid.Children.Add(browser);
            acquireRequestToken();

        }

        private void acquireRequestToken()
        {
            OAuthResponse reqToken = manager.AcquireRequestToken("https://publicapi.avans.nl/oauth/request_token", "POST");
            baseUrl = "https://publicapi.avans.nl/oauth/login.php?" + reqToken.AllText;
            browser.myBrowser.Navigate(new Uri(baseUrl));
            // Console.WriteLine(browser.myBrowser.Source.AbsoluteUri.ToString());

        }

        private void checkLogin()
        {
            try
            {
                OAuthResponse accessToken = manager.AcquireAccessToken("https://publicapi.avans.nl/oauth/access_token", "GET", _pin);
                string search = "https://publicapi.avans.nl/oauth/api/user/?format=xml";
                var authzHeader = manager.GenerateAuthzHeader(search, "GET");
                var request = (HttpWebRequest)WebRequest.Create(search);
                request.Method = "GET";
                request.PreAuthenticate = true;
                request.AllowWriteStreamBuffering = true;
                request.Headers.Add("Authorization", authzHeader);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show("There's been a problem trying to tweet:" +
                                        Environment.NewLine +
                                        response.StatusDescription);
                    }
                    else
                    {
                        login = "";

                        Stream responseStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(responseStream);
                        string logininfo = reader.ReadToEnd();
                        reader.Close();

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(logininfo);

                        XmlElement root = doc.DocumentElement;

                        XmlNodeList loginNodes = root.SelectNodes("//login");

                        foreach (XmlNode node in loginNodes)
                        {
                            login = node.InnerText;
                        }
                        checkTeacher();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void checkTeacher()
        {
            string search = "https://publicapi.avans.nl/oauth/telefoongids/" + login + "/?format=xml";
            var authzHeader = manager.GenerateAuthzHeader(search, "GET");
            var request = (HttpWebRequest)WebRequest.Create(search);
            request.Method = "GET";
            request.PreAuthenticate = true;
            request.AllowWriteStreamBuffering = true;
            request.Headers.Add("Authorization", authzHeader);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("There's been a problem trying to tweet:" +
                                    Environment.NewLine +
                                    response.StatusDescription);
                }
                else
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    string result = reader.ReadToEnd();
                    if (result.Equals("null"))
                    {
                        function = "Student";
                    }
                    else
                    {
                        function = "Docent";
                    }
                }
            }
        }

        private void tryDefaultLogin()
        {
            if (username.Equals("admin"))
            {
                if (DatabaseConnection.checkLoginCredentials(userName_TextBox.Text, encrypter.MD5Hash(password_PasswordBox.Password)) == true)
                {
                    function = "Coördinator";
                    loggedIn = true;
                    Login();
                }
                else
                {
                    MessageBox.Show("Gebruikersnaam/wachtwoord niet gevonden. Probeer het nog eens.", "Ongeldige gebruikersnaam/wachtwoord");
                    browser.loadCount = 1;
                }
            }
            else if (username.Equals("docent"))
            {
                function = "Docent";
                loggedIn = true;
                Login();
            }
            else if (username.Equals("student"))
            {
                function = "Student";
                loggedIn = true;
                Login();
            }
            else
            {
                automateStudentLogin();
            }
        }

        private void automateStudentLogin()
        {

            mshtml.HTMLDocument doc = (mshtml.HTMLDocument)browser.myBrowser.Document;
            doc.getElementById("login").setAttribute("value", userName_TextBox.Text);
            doc.getElementById("password").setAttribute("value", password_PasswordBox.Password);

            mshtml.IHTMLElementCollection coll = doc.all;

            foreach (mshtml.IHTMLElement element in coll)
            {
                if (element.sourceIndex == 32)
                {
                    element.click();
                }
            }
        }

        public Boolean tryStudentLogin()
        {
            if (Url.Contains("verifier"))
            {
                string[] splits = Url.Split('=');
                _pin = splits[splits.Length - 1];
                checkLogin();
                password = "";
                username = "";
                loggedIn = true;
            }
            else if (Url.Contains("Ongeldige"))
            {
                loggedIn = false;
                resetBrowser();
            }


            return loggedIn;
        }

        private void resetBrowser()
        {
            if (Url != baseUrl)
            {
                browser.myBrowser.Navigate(new Uri(baseUrl));
            }
        }




        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.sessionModel = new Model.Session();

            username = userName_TextBox.Text;
            password = password_PasswordBox.Password;
            try
            {
                tryDefaultLogin();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        public void Login()
        {
            if (loggedIn)
            {
                sessionModel.login(username, function);
                MainWindow mainWindow = new MainWindow(sessionModel);
                mainWindow.Visibility = Visibility.Visible;
                this.Close();
            }
            else
            {
                MessageBox.Show("Gebruikersnaam/wachtwoord niet gevonden. Probeer het nog eens.", "Ongeldige gebruikersnaam/wachtwoord");
                browser.loadCount = 1;
            }
        }

    }
}

