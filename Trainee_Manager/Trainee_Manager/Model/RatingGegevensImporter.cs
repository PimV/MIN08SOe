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

        public void CreateDocenten()
        {
            docentList = new DocentList();

            foreach (DataRow dr in docentenDT.Rows)
            {

                docent = new Docent();

                foreach (DataColumn dc in docentenDT.Columns)
                {
                    //Console.WriteLine(dc.ColumnName);
                    if (dr[dc.ColumnName].ToString() != string.Empty)
                    {
                        switch (dc.ColumnName)
                        {
                            case "Docent":
                                docent.Naam = dr[dc.ColumnName].ToString();
                                break;

                            case "ID":
                                docent.Id = Convert.ToInt32(dr[dc.ColumnName].ToString());
                                break;

                            case "Email":
                                docent.Email = dr[dc.ColumnName].ToString();
                                break;

                            case "Adres":
                                docent.Adres = dr[dc.ColumnName].ToString();
                                break;

                            case "Postcode":
                                docent.Postcode = dr[dc.ColumnName].ToString();
                                break;

                            case "vrijeuren":
                                docent.Vrije_uren = Convert.ToInt32(dr[dc.ColumnName].ToString());
                                break;

                            case "periode":
                                docent.Periode = Convert.ToInt32(dr[dc.ColumnName].ToString());
                                break;

                            case "vkbedrijnfnaam":
                                docent.VoorkeurBedrijven.Add(dr[dc.ColumnName].ToString());
                                break;

                            case "stageID":
                                //Console.WriteLine("StageID :" + dr[dc.ColumnName].ToString());
                                  docent.VoorkeurStages.Add(Convert.ToInt32(dr[dc.ColumnName].ToString()));
                                break;

                            case "Kenmerk":
                                splitKenmerken(dr[dc.ColumnName].ToString());
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

                while (!characters[i].Equals(","))
                {
                    kenmerk += characters[i];
                }

                if (characters[i].Equals(","))
                {
                    docent.kenmerken.Add(kenmerk);

                    kenmerk = "";
                }
            }

        }

        //Call the procedure to load the mysql data
        private void getData()
        {
            docentenDT = DatabaseConnection.commandSelect("CALL procedure_docent_overzicht_ratingsysteem()");
        }

    }
}
