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
    public class RatingGegevensImporter
    {
        private static DataTable docentenDT;
        private static DataTable stageOpdrachtDT;

        private DocentList docentList;
        private Docent docent;
        private Bedrijf bedrijf;
        private Student student;


        public RatingGegevensImporter(int stageID)
        {
            getData(stageID);
            CreateDocenten();
            CreateOpdrachten();
        }

        public void CreateOpdrachten()
        {
            bedrijf = new Bedrijf();
            Student student = new Student();
            Student student2 = new Student();
            StageOpdracht stageOpdracht = new StageOpdracht();
            foreach (DataRow dr in stageOpdrachtDT.Rows)
            {

                foreach (DataColumn dc in stageOpdrachtDT.Columns)
                {

                    if (dr[dc.ColumnName].ToString() != string.Empty)
                    {
                        switch (dc.ColumnName)
                        {
                            case "BedNaam":
                                bedrijf.Naam = dr[dc.ColumnName].ToString();
                                Console.WriteLine("BEdrijfsnaam: " + bedrijf.Naam);
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

                            case "Student2":
                                student2.Studentid = Convert.ToInt32(dr[dc.ColumnName].ToString());
                                break;
                            case "NaamStudent2":
                                student2.Naam = dr[dc.ColumnName].ToString();
                                break;
                            case "StudentnummerStudent2":
                                student2.Studentnummer = Convert.ToInt32(dr[dc.ColumnName].ToString());
                                break;
                            case "StageID":
                                stageOpdracht.Student1 = student;
                                stageOpdracht.Student2 = student2;
                                stageOpdracht.Bedrijf = bedrijf;
                                stageOpdracht.StageID = Convert.ToInt32(dr[dc.ColumnName].ToString());
                                break;
                            default:
                                break;


                        }
                    }
                }

                RatingCalculator calc = new RatingCalculator(docentList, stageOpdracht);

            }
            student2 = null;
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

                                string[] stringParts = dr[dc.ColumnName].ToString().Split(',');
                                foreach (string s in stringParts)
                                {
                                    int stageID;
                                    Int32.TryParse(s.Trim(), out stageID);
                                    if (stageID != 0)
                                    {

                                        docent.VoorkeurStages.Add(stageID);
                                    }
                                }
                                //  MessageBox.Show(dr[dc.ColumnName].ToString());

                                break;

                            case "kenmerk":
                                splitKenmerken(dr[dc.ColumnName].ToString());
                                break;
                        }


                    }
                }
                if (docent != null)
                    docentList.DocentenList.Add(docent);
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
        private void getData(int stageID)
        {
            docentenDT = DatabaseConnection.commandSelect("CALL procedure_docent_overzicht_ratingsysteem()");
            stageOpdrachtDT = DatabaseConnection.commandSelect("CALL procedure_gegevens_ratingsysteem(" + stageID + ")");
        }

    }
}
