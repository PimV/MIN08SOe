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
using System.Xml;

namespace DistanceMeasurer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private String timeText;
        private String timeValue;
        private String distText;
        private String distValue;
        private String origin;
        private String destination;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalcDistButton(object sender, RoutedEventArgs e)
        {
            String from = fromBox.Text;
            from = from.Replace(' ', '_');
            from = from.Trim();

            String to = toBox.Text;
            to = to.Replace(' ', '_');
            to = to.Trim();
            Console.WriteLine("From: " + from + ":: To: " + to);


            collectData(from, to);

            showData();



        }

        private void showData()
        {
            distLabel.Content = distValue + "\n" + distText;
            timeLabel.Content = timeValue + "\n" + timeText;
            fromLabel.Content = origin;
            toLabel.Content = destination;
        }

        private Boolean collectData(String from, String to)
        {
            origin = "";
            destination = "";
            timeText = "";
            timeValue = "";
            distText = "";
            distValue = "";

            String URLString = "http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + from + "&destinations=" + to + "&mode=driving&language=nl-FR&sensor=false";

            XmlTextReader reader = new XmlTextReader(URLString);

            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            XmlElement root = doc.DocumentElement;

            XmlNodeList originNodes = root.SelectNodes("//DistanceMatrixResponse/origin_address");

            foreach (XmlNode node in originNodes)
            {
                origin = node.InnerText;
            }

            XmlNodeList destNodes = root.SelectNodes("//DistanceMatrixResponse/destination_address");

            foreach (XmlNode node in destNodes)
            {
                destination = node.InnerText;
            }

            XmlNodeList timeNodes = root.SelectNodes("//DistanceMatrixResponse/row/element/duration");
            foreach (XmlNode node in timeNodes)
            {

                timeValue = node["value"].InnerText + "s";
                timeText = node["text"].InnerText;
            }

            XmlNodeList distNodes = root.SelectNodes("//DistanceMatrixResponse/row/element/distance");
            foreach (XmlNode node in distNodes)
            {

                distValue = node["value"].InnerText + "m";
                distText = node["text"].InnerText;
            }
            if (timeText.Equals(""))
            {
                MessageBox.Show("Could not find either the destination or the origin address");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void EnterButton(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CalcDistButton(sender, e);
            }
        }
    }
}
