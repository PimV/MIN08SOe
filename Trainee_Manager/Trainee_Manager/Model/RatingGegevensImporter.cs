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

        private RatingController _ratingController;

        public RatingGegevensImporter(int stageID, RatingController _ratingController)
        {
            this._ratingController = _ratingController;
            getData(stageID);
            CreateDocenten();
            CreateOpdrachten();
        }

        public void CreateOpdrachten()
        {
            Bedrijf bedrijf = new Bedrijf();
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

                            case "Afstudeerder":
                                Boolean IsAfstudeerOpdracht = Convert.ToBoolean(dr[dc.ColumnName].ToString());

                                if (IsAfstudeerOpdracht)
                                {
                                    stageOpdracht.AfstudeerOpdracht = IsAfstudeerOpdracht;
                                }

                                else
                                {
                                    stageOpdracht.AfstudeerOpdracht = IsAfstudeerOpdracht;
                                }
                                break;

                            case "EPS":
                                stageOpdracht.EPS = Convert.ToBoolean(dr[dc.ColumnName].ToString());
                                break;

                            case "PeriodeID":
                                stageOpdracht.Periode = Convert.ToInt32(dr[dc.ColumnName].ToString());
                                break;

                            case "kenmerk":
                                string[] stringParts = split(dr[dc.ColumnName].ToString());

                                foreach (string s in stringParts)
                                {
                                    stageOpdracht.kenmerken.Add(s.Trim());
                                }
                                break;
                        }
                    }
                }
            }
            RatingCalculator calc = new RatingCalculator(docentList, stageOpdracht);
            _ratingController.setCalc(calc);
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

                            case "vrij_uren":
                                MessageBox.Show((dr[dc.ColumnName].ToString()));
                               // docent.Vrije_uren.Add(dr[dc.ColumnName].ToString()
                                splitVrijeuren(dr[dc.ColumnName].ToString());
                                break;

                            case "vkbedrijfnaam":
                                string[] Voorkeurparts = split(dr[dc.ColumnName].ToString());

                                foreach (string s in Voorkeurparts)
                                {
                                    docent.VoorkeurBedrijven.Add(s.Trim());
                                }
                                break;

                            case "stageID":
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
                                break;

                            case "kenmerk":
                                docent.Kenmerken = dr[dc.ColumnName].ToString();

                                string[] docentKenmerken = split(dr[dc.ColumnName].ToString());

                                foreach (string s in docentKenmerken)
                                {
                                    docent.kenmerkenlijst.Add(s.Trim());
                                }
                                break;
                        }
                    }
                }
                if (docent != null)
                    docentList.DocentenList.Add(docent);
            }
        }

        //splits de inkomende string in apparte woordjes en returned dit.
        private string[] split(string input)
        {
            string[] stringParts = input.Split(',');

            return stringParts;
        }

        private void splitVrijeuren(String input)
        {
            List<KeyValuePair<int, string>> items = new List<KeyValuePair<int, string>>();

            string[] splitParts = split(input);
            List<string> parts = new List<string>();


            foreach (string s in splitParts)
            {
                string[] splitsplitParts = s.Split('|');

                foreach (string d in splitsplitParts)
                {
                    parts.Add(d.Trim());
                }
            }

            for (int i = 0; i < parts.Count - 1; i = i + 2)
            {
                Console.WriteLine(parts[i] + " " + parts[i + 1]);
                items.Add(new KeyValuePair<int, string>(Int32.Parse(parts[i]), parts[i+1]));
            }

            docent.addVrijurenList(items);
        }

        //Call the procedure to load the mysql data
        private void getData(int stageID)
        {
            docentenDT = DatabaseConnection.commandSelect("CALL procedure_docent_overzicht_ratingsysteem(" + stageID + ")");
            stageOpdrachtDT = DatabaseConnection.commandSelect("CALL procedure_gegevens_ratingsysteem(" + stageID + ")");
        }

    }
}
