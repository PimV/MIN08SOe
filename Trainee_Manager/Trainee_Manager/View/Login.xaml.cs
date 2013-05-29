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
using System.Xml.Linq;
using Trainee_Manager.Controller;
using Trainee_Manager.ViewModel;

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
        private string id = "";

        private string baseUrl;
        public string BaseUrl
        {
            get { return baseUrl; }
            set { baseUrl = value; }
        }

        private Browser browser;

        private Model.Session sessionModel;

        //private static Controller.DatabaseConnection connection;
        private Controller.MD5Encrypter encrypter;

        /*
         * Constructor for the Login class, in which the view is made and the data checked.
         */
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
            // if (App.IsConnectedToInternet())
            // {
            acquireRequestToken();
            // }
        }

        /*
         * 
         * Tries to log the user in with the specified username, but only if the
         * username/password combination exists in either the Avans OAuth or in our
         * user database.  
         * 
         */
        public void Login()
        {
            if (loggedIn)
            {
                sessionModel.login(username, function);
                MainWindow mainWindow;
                if (function == "Student")
                {
                    mainWindow = new MainWindow(sessionModel, id);
                }
                else
                {
                    mainWindow = new MainWindow(sessionModel);
                }



                mainWindow.Visibility = Visibility.Visible;
                BaseUrl = null;
                Console.WriteLine("asfjaksdfjaksjf");
                this.Close();
            }
            else
            {
                MessageBox.Show("Gebruikersnaam/wachtwoord niet gevonden. Probeer het nog eens.", "Ongeldige gebruikersnaam/wachtwoord", MessageBoxButton.OK, MessageBoxImage.Warning);
                browser.loadCount = 1;
            }
        }

        /*
         * Validates the entered username/login if the user is either
         * a student or a teacher. Returns whether this user can be logged in
         * or not.
         */
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
                checkID();
            }
            else if (Url.Contains("Ongeldige"))
            {
                loggedIn = false;
                resetBrowser();
            }


            return loggedIn;
        }

        /// <summary>
        /// OAuth Method. Acquires the request token needed to access the Avans OAuth.
        /// </summary>
        /////  <param name="">a dataset, passed by reference, 
        ///// that contains all the 
        ///// data for updating</param>

        public void acquireRequestToken()
        {
            if (App.IsConnectedToInternet())
            {
                try
                {
                    OAuthResponse reqToken = manager.AcquireRequestToken("https://publicapi.avans.nl/oauth/request_token", "POST");
                    baseUrl = "https://publicapi.avans.nl/oauth/login.php?" + reqToken.AllText;
                    navigateBrowser();

                }
                catch
                {
                    //MessageBox.Show("Let op! Internetverbinding gedetecteerd, maar geen toegang tot het internet herkend. Inloggen mislukt.", "Inloggen mislukt", MessageBoxButton.OK, MessageBoxImage.Error);

                }




            }
            else
            {
                MessageBox.Show("Let op! Geen internetverbinding gedetecteerd.", "Geen internetverbinding", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        public void navigateBrowser()
        {
            try
            {
                if (App.IsConnectedToInternet() && BaseUrl != null)
                {
                    browser.myBrowser.Navigate(new Uri(BaseUrl));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }


        }

        private void checkID()
        {
            try
            {
                // OAuthResponse accessToken = manager.AcquireAccessToken("https://publicapi.avans.nl/oauth/access_token", "GET", _pin);
                string search = "https://publicapi.avans.nl/oauth/resultaten/v2/?format=xml";
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

                        string logininfo = reader.ReadToEnd();
                        reader.Close();

                        //  Console.WriteLine(logininfo);
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(logininfo);

                        //  XDocument xdoc = XDocument.Parse(logininfo);
                        //   Console.WriteLine(xdoc.ToString());

                        XmlElement root = doc.DocumentElement;

                        XmlNodeList loginNodes = root.GetElementsByTagName("typ:studentnummer");

                        id = "";

                        foreach (XmlNode node in loginNodes)
                        {

                            if (id.Equals(""))
                            {
                                id = node.InnerText;
                                Console.WriteLine("Studentnummer: " + id);
                                break;
                            }


                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /*
         * Gets the users login name and initiates the teacher's check.
         */
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


        /*
         * Checks whether the currently logged in user is a teacher or not.
         */
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

        /*
         * Determines which login procedure the application should follow, 
         * after the user entered their username and password.
         */

        private void tryDefaultLogin()
        {
            if (username.Equals("admin"))
            {
                if (DatabaseConnection.checkLoginCredentials(userName_TextBox.Text, encrypter.MD5Hash(password_PasswordBox.Password)) == true)
                {
                    Console.WriteLine("Hoi");
                    function = "Coördinator";
                    loggedIn = true;
                    Login();
                }
                else
                {
                    MessageBox.Show("Gebruikersnaam/wachtwoord niet gevonden. Probeer het nog eens.", "Ongeldige gebruikersnaam/wachtwoord", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        /*
         * Automates the process of entering the OAuth form and verificating their data.
         */
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



        /*
         * Resets the browser URL to the initial OAuth URL.
         */
        private void resetBrowser()
        {
            if (Url != baseUrl)
            {
                browser.myBrowser.Navigate(new Uri(baseUrl));
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.IsConnectedToInternet())
            {
                if (BaseUrl == null)
                {
                    acquireRequestToken();
                }

                this.sessionModel = new Model.Session();

                username = userName_TextBox.Text;
                password = password_PasswordBox.Password;
                try
                {
                    tryDefaultLogin();
                    // BaseUrl = null;

                }
                catch (Exception exception)
                {
                    //MessageBox.Show(exception.ToString());
                    navigateBrowser();
                    MessageBox.Show("Er ging iets mis met het inloggen. Probeer het opnieuw.");
                }

            }
            else
            {

                MessageBox.Show("Let op! Geen internetverbinding gedetecteerd.", "Geen internetverbinding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}

