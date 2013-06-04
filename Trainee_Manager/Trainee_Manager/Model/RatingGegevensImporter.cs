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
        private static DataTable stageOpdrachtDT;

        private DocentList docentList;
        private Docent docent;
        private Bedrijf bedrijf;
        private Student student;

        public RatingGegevensImporter()
        {
            getData();
        }

        public void CreateOpdrachten()
        {
            bedrijf = new Bedrijf();
            student = new Student();

            foreach (DataRow dr in stageOpdrachtDT.Rows)
            {

                foreach (DataColumn dc in docentenDT.Columns)
                {

                    if (dr[dc.ColumnName].ToString() != string.Empty)
                    {
                        switch (dc.ColumnName)
                        {
                            case "BedNaam":
                                bedrijf.Naam = dr[dc.ColumnName].ToString();
                                break;

                            case "BedPlaats":
                                bedrijf.Plaats = dr[dc.ColumnName].ToString();
                                break;

                            case "BedPostcode":
                                bedrijf.Postcode = dr[dc.ColumnName].ToString();
                                break;

                            case "BedStraat":
                                bedrijf.Straat = dr[dc.ColumnName].ToString();
                                break;

                            case "Student1":
                                student.Studentid = Convert.ToInt32(dr[dc.ColumnName].ToString());
                                break;

                            case "NaamStudent1":
                                student.Naam = dr[dc.ColumnName].ToString();
                                break;

                            case "StudentnummerStudent1":
                                student.Studentnummer = Convert.ToInt32(dr[dc.ColumnName].ToString());
                                break;




                        }
                    }
                }
            }
        }

        //leest de docntenDT datatable uit en maakt hiervoor iedere docent die hij tegenkomt een docentobject aan.
        public void CreateDocenten()
        {
            docentList = new DocentList();

            foreach (DataRow dr in docentenDT.Rows)
            {

                docent = new Docent();

                foreach (DataColumn dc in docentenDT.Columns)
                {
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

                            case "vkbedrijfnaam":
                                splitVoorkeurBedrijven(dr[dc.ColumnName].ToString());
                                break;

                            case "stageID":
                                //dit moet nog aangepast worden na procedure aanpassing
                                  docent.VoorkeurStages.Add(Convert.ToInt32(dr[dc.ColumnName].ToString()));
                                break;

                            case "kenmerk":
                                splitKenmerken(dr[dc.ColumnName].ToString());
                                break;
                        }
                    }
                }
            }
        }


        // De kenmerken komen binnen als 1 string, 
        // deze word hierin gesplit tot één kenmerk en geplaats in de docent.kenmerken array.
        private void splitKenmerken(string input)
        {
            string[] stringParts = input.Split(',');

            foreach (string s in stringParts)
            {
                docent.kenmerken.Add(s.Trim());
            }
        }

        // De kenmerken komen binnen als 1 string, 
        // deze worden hierin gesplit tot één kenmerk en geplaats in de docenten.VoorkeurBedrijven array.
        private void splitVoorkeurBedrijven(string input)
        {
            string[] stringParts = input.Split(',');

            foreach (string s in stringParts)
            {
                docent.VoorkeurBedrijven.Add(s.Trim());
            }      
        }

        //Call the procedure to load the mysql data
        private void getData()
        {
            docentenDT = DatabaseConnection.commandSelect("CALL procedure_docent_overzicht_ratingsysteem()");
            stageOpdrachtDT = DatabaseConnection.commandSelect("CALL procedure_gegevens_ratingsysteem()");
        }

    }
}
