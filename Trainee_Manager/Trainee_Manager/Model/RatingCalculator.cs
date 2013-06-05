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
    class RatingCalculator
    {
        private StageOpdracht opdracht;
        private DocentList docenten;

        public RatingCalculator(DocentList docenten, StageOpdracht opdracht)
        {
            this.docenten = docenten;
            this.opdracht = opdracht;
            calculate();
        }

        private void calculate()
        {
            Console.WriteLine(docenten.DocentenList.Count);
            foreach (Docent doc in docenten.DocentenList)
            {
                if (doc.VoorkeurStages.Contains(opdracht.StageID))
                {
                    improveRating("PerseeStudent", doc);
                }



                Console.WriteLine(doc.Naam + " : " + doc.Rating);

                
            }
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
