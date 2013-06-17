using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trainee_Manager.Controller;

namespace Trainee_Manager.Model
{
    public class RatingCalculator
    {
        private StageOpdracht opdracht;

        public StageOpdracht Opdracht
        {
            get { return opdracht; }
            set { opdracht = value; }
        }
        private DocentList docenten;

        public DocentList Docenten
        {
            get { return docenten; }
            set { docenten = value; }
        }

        private String reisinfo;

        public RatingCalculator(DocentList docenten, StageOpdracht opdracht)
        {
            this.docenten = docenten;
            this.opdracht = opdracht;

            foreach (Docent doc in docenten.DocentenList)
            {
                if (!opdracht.EPS)
                {
                    foreach (KeyValuePair<int, string> kvp in doc.Vrije_uren)
                    {
                        if (kvp.Key == opdracht.Periode)
                        {
                            doc.Tijdvrij = Convert.ToInt32(kvp.Value);
                        }
                    }

                    if (opdracht.AfstudeerOpdracht == true)
                    {
                        if (doc.Tijdvrij >= 20)
                        {
                            improveRating("vrijeuren", doc);
                            calculate(doc);
                        }
                    }

                    else
                    {
                        if (doc.Tijdvrij >= 16)
                        {
                            improveRating("vrijeuren", doc);
                            calculate(doc);
                        }
                    }
                }
            }
        }

        private void calculate(Docent doc)
        {
            // vergelijkt de docent voorkeurstages aan de hand van het stageID
            // als dit overeenkomt improverating
            if (doc.VoorkeurStages.Contains(Opdracht.StageID))
            {
                improveRating("PerseeStudent", doc);
            }

            // vergelijkt de voorkeursbedrijven van de docent met het bedrijf van de stage opdracht
            // als dit matched improverating
            if (doc.VoorkeurBedrijven.Contains(Opdracht.Bedrijf.Naam))
            {
                improveRating("voorkeur", doc);
            }

            //vereglijk de kenmerken aan elkaar als dit matched improverating
            if (doc.kenmerkenlijst != null && Opdracht.Kenmerken != null)
            {
                int aantalmatches = 0;

                foreach (string s in Opdracht.Kenmerken)
                {
                    if (doc.kenmerkenlijst.Contains(s))
                    {
                        aantalmatches++;
                    }
                }

                if (aantalmatches > 0)
                {
                    improveRating("kennis", doc);
                }
            }

            // kijkt naar de afstand van de docent naar het bedrijf.
            // als de reistijd onder 30 minuten is improverating ( de tijd komt terug als secondes)
            checkDistance(doc, opdracht);
            String[] reisInfoSplit = reisinfo.Split('/');

            int reistijd;
            Int32.TryParse(reisInfoSplit[0], out reistijd);

            String reisAfstand = reisInfoSplit[1];
            doc.AfstandInt = reistijd;
            if (reistijd == 0)
            {
                MessageBox.Show("HOI");
                reisAfstand = "-";
            }
            doc.Afstand = reisAfstand;
            if (reistijd <= 1800)
            {
                improveRating("afstand", doc);
            }

            DataTable test = DatabaseConnection.commandSelect("SELECT COUNT(*) FROM stages WHERE docent_id='" + doc.Id + "' AND bedrijf_id='" + opdracht.Bedrijf.Bedrijf_id + "'");
            int relatieInt;
            Int32.TryParse(test.Rows[0][0].ToString(), out relatieInt);
            doc.Relatie = relatieInt;


            Console.WriteLine(doc.Naam + " : " + doc.Rating);
        }

        public String checkDistance(Docent doc, StageOpdracht opdracht)
        {
            string docadres = doc.Adres + "," + doc.Postcode;
            string stageadres = opdracht.Bedrijf.Straat + "," + opdracht.Bedrijf.Postcode;

            reisinfo = DistanceController.collectData(docadres, stageadres);

            return reisinfo;
        }


        public void improveRating(String input, Docent docent)
        {
            switch (input)
            {

                case "afstand":
                    docent.Rating += 25;
                    break;

                case "voorkeur":
                    docent.Rating += 50;
                    break;

                case "kennis":
                    docent.Rating += 75;
                    break;

                case "vrijeuren":
                    docent.Rating += 100;
                    break;

                case "PerseeStudent":
                    docent.Rating += 1000;
                    break;
            }
        }
    }
}
