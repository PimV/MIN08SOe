using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Trainee_Manager.Controller
{
    public class DistanceController
    {

        public static String collectData(String from, String to)
        {
            int timeValue = -1 ;

            String URLString = "http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + from + "&destinations=" + to + "&mode=driving&language=nl-FR&sensor=false";

            XmlTextReader reader = new XmlTextReader(URLString);

            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            XmlElement root = doc.DocumentElement;            

            XmlNodeList timeNodes = root.SelectNodes("//DistanceMatrixResponse/row/element/duration");
            foreach (XmlNode node in timeNodes)
            {
                int time;
                Int32.TryParse(node["value"].InnerText, out time);
                if (time > 0)
                {
                    timeValue = time;
                }
                //timeText = node["text"].InnerText;
            }

            //if (timeText.Equals(""))
            //{
            //    return -1;
            //}
            //else
            //{
            //    return timeValue;
            //}
            String distText = "";
            XmlNodeList distNodes = root.SelectNodes("//DistanceMatrixResponse/row/element/distance");
            foreach (XmlNode node in distNodes)
            {

                //distValue = node["value"].InnerText + "m";
                distText = node["text"].InnerText;
            }
            String toReturn = timeValue + "/" + distText;
            return toReturn;
        }

    }
}
