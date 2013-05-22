using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;

namespace OAUTHTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _url;
        public string Url
        {
            get
            {
                return myBrowser.Source.AbsoluteUri.ToString();
            }
            set
            {
                value = _url;
            }
        }

        private Manager manager;

        private string _pin;

        private string login = "";

        private Boolean isDocent;

        public MainWindow()
        {
            InitializeComponent();
            manager = new Manager();
            acquireRequestToken();
        }

        private void acquireRequestToken()
        {
            OAuthResponse reqToken = manager.AcquireRequestToken("https://publicapi.avans.nl/oauth/request_token", "POST");
            string url = "https://publicapi.avans.nl/oauth/login.php?" + reqToken.AllText;
            myBrowser.Navigate(new Uri(url));
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

        private void myBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            Url = myBrowser.Source.AbsoluteUri.ToString();
            if (Url.Contains("verifier"))
            {
                string[] splits = Url.Split('=');
                _pin = splits[splits.Length - 1];
                checkLogin();
            }

            string script = "document.body.style.overflow ='hidden'";
            WebBrowser wb = (WebBrowser)sender;
            wb.InvokeScript("execScript", new Object[] { script, "JavaScript" });

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
                        isDocent = false;
                        MessageBox.Show("U bent geen docent (ALS HET GOED IS!)");
                    }
                    else
                    {
                        isDocent = true;
                        MessageBox.Show(result);
                        MessageBox.Show("Als u een docent bent, dan werkt dit!");
                    }
                }
            }
        }
    }
}
