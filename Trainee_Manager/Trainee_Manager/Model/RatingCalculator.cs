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
                        if (doc.VoorkeurStages.Contains(opdracht.StageID))
                        {
                            calculate(doc);
                        }

                        else if (doc.Tijdvrij >= 20)
                        {
                            improveRating("vrijeuren", doc);
                            calculate(doc);
                        }
                    }

                    else
                    {
                        if (doc.VoorkeurStages.Contains(opdracht.StageID))
                        {
                            calculate(doc);
                        }

                        else if (doc.Tijdvrij >= 16)
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
            if (doc.VoorkeurStages.Contains(opdracht.StageID))
            {
                
                improveRating("PerseeStudent", doc);
            }

            if (doc.VoorkeurBedrijven.Contains(opdracht.Bedrijf.Naam))
            {
                improveRating("voorkeur", doc);
            }

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
            Console.WriteLine(doc.Naam + " : " + doc.Rating);
        }


        public void improveRating(String input, Docent docent)
        {
            switch (input)
            {

                case "VakerBijBedrijfGeweest":
                    docent.Rating += 10;
                    break;

                case "afstandtwee":
                    docent.Rating += 15;
                    break;

                case "afstandeen":
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

                case "MeerdereStudentenBijHetzelfdeBedrijf":
                    docent.Rating += 100;
                    break;

                case "PerseeStudent":
                    docent.Rating += 1000;
                    break;

                default:
                    docent.Rating += 0;
                    break;
            }
        }
    }
}
