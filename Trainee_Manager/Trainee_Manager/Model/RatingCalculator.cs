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
        private DocentList docenten;

        public DocentList Docenten
        {
            get { return docenten; }
            set { docenten = value; }
        }

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
            if (doc.VoorkeurStages.Contains(opdracht.StageID))
            {   
                improveRating("PerseeStudent", doc);
            }

            // vergelijkt de voorkeursbedrijven van de docent met het bedrijf van de stage opdracht
            // als dit matched improverating
            if (doc.VoorkeurBedrijven.Contains(opdracht.Bedrijf.Naam))
            {
                improveRating("voorkeur", doc);
            }

            //vereglijk de kenmerken aan elkaar als dit matched improverating
            if (doc.kenmerkenlijst != null && opdracht.Kenmerken != null)
            {
                int aantalmatches = 0;

                foreach (string s in opdracht.Kenmerken)
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
            if (checkDistance(doc, opdracht) <= 1800)
            {
                improveRating("afstand", doc);
            }


            Console.WriteLine(doc.Naam + " : " + doc.Rating);
        }

        public int checkDistance(Docent doc, StageOpdracht opdracht)
        {
            string docadres = doc.Adres + "," + doc.Postcode;
            string stageadres = opdracht.Bedrijf.Straat + "," + opdracht.Bedrijf.Postcode;

            int reistijd = DistanceController.collectData(docadres, stageadres);

            return reistijd;
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
