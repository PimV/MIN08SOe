using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainee_Manager.Controller;

namespace Trainee_Manager.Model
{
    class RatingCalculator
    {
        private StageOpdracht opdracht;
        private Docent docent;

        public RatingCalculator()
        {
            getOpdracht();
        }

        private void getOpdracht()
        {
            opdracht = new StageOpdracht();
            
            //foreach student koppeld aan de opdracht

            getDocentData();
        }

        private void getDocentData()
        { 
            //foreach docent die bestaat
            docent = new Docent();

        }

        public void improveRating(String preference)
        { 
            switch(preference)
            {

                case "VakerBijBedrijfGeweest":
                    docent.Rating += 10;
                    break;

                case "afstandtwee":
                    docent.Rating += 15;
                    break;

                case "afstandeen":
                    docent.Rating +=25;
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
