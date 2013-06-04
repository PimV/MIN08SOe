using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainee_Manager.Controller;

namespace Trainee_Manager.Model
{
    public class RatingGegevensImporter
    {
        private static DataTable docentenDT;

        private DocentList docentList;
        private Docent docent;

        public RatingGegevensImporter()
        {
            getData();
        }

        private void CreateDocenten()
        {
            docentList = new DocentList();

            foreach (DataRow dr in docentenDT.Rows)
            {
                if (dr != docentenDT.Rows[0] && dr != docentenDT.Rows[docentenDT.Rows.Count - 1])
                {
                   docent = new Docent();

                   foreach (DataColumn dc in docentenDT.Columns)
                   {
                       switch (docentenDT.Columns[0].ToString())
                       { 
                           case "Docent":
                               docent.Naam = dc.ToString();
                               break;

                           case "ID":
                               docent.Id = Convert.ToInt32(dc.ToString());
                               break;

                           case "Email":
                               docent.Email = dc.ToString();
                               break;

                           case "Adres":
                               docent.Adres = dc.ToString();
                               break;

                           case "Postcode":
                               docent.Postcode = dc.ToString();
                               break;

                           case "vrijeuren":
                               docent.Vrije_uren = Convert.ToInt32(dc.ToString());
                               break;

                           case "periode":
                               docent.Periode = Convert.ToInt32(dc.ToString());
                               break;

                           case "vkbedrijnfnaam":
                               docent.VoorkeurBedrijven.Add(dc.ToString());
                               break;

                           case "stageID":
                               docent.VoorkeurStages.Add(Convert.ToInt32(dc.ToString()));
                               break;

                           case "Kenmerk":
                               splitKenmerken(dc.ToString());
                               break;
                        
                       }
                   }
                }
            }   
        }

        private void splitKenmerken(string input)
        {
            char[] characters = input.ToCharArray();

            for (int i = 0; 1 < characters.Length; i++)
            {
                string kenmerk = "";

                while(!characters[i].Equals(","))
                {
                    kenmerk += characters[i];
                }

                if(characters[i].Equals(","))
                {
                    docent.kenmerken.Add(kenmerk);

                    kenmerk = "";
                }
            }

        }

        //Call the procedure to load the mysql data
        private void getData()
        {
            docentenDT = DatabaseConnection.commandSelect("CALL procedure_docent_overzicht_ratingsysteem");
        }

    }
}
