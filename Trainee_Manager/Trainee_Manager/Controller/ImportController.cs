﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trainee_Manager.Controller;
using Trainee_Manager.Model;

namespace Trainee_Manager.Controller
{
    public class ImportController
    {

        private ExcelStageModel stageModel;
        private ExcelDocentenModel docentModel;
        private ExcelUrenModel urenModel;

        private string start1;
        public string Start1 { get { return start1; } set { start1 = value; } }

        private string end1;
        public string End1 { get { return end1; } set { end1 = value; } }

        private string periodeNaam;
        public string PeriodeNaam { get { return periodeNaam; } set { periodeNaam = value; } }

        private int periode;
        public int Periode { get { return periode; } set { periode = value; } }

        private int opleidingID;

        public int OpleidingID
        {
            get { return opleidingID; }
            set { opleidingID = value; }
        }


        public ImportController()
        {
            stageModel = new ExcelStageModel();
            docentModel = new ExcelDocentenModel();
            urenModel = new ExcelUrenModel();
        }

        public Boolean checkStageLijst(string fileName)
        {
            Boolean passed = false;
            stageModel.Periode = Periode + "-" + PeriodeNaam;
            stageModel.OpleidingID = OpleidingID;

            try
            {
                if (fileName.EndsWith(".xlsx"))
                {
                    if (stageModel.XLSX(fileName))
                    {
                        stageModel.toDB();
                        passed = true;
                    }
                }
                else if (fileName.EndsWith(".xls"))
                {
                    if (stageModel.XLS(fileName))
                    {
                        stageModel.toDB();
                        passed = true;
                    }
                }
                else
                {
                    MessageBox.Show("File type not supported");
                    passed = false;
                }
            }
            catch
            {
                MessageBox.Show("Importing failed. Is the Excel sheet damaged or not safe?");
                passed = false;
            }
            return passed;
        }

        public void checkDocentenLijst(string fileName)
        {
            docentModel.OpleidingID = OpleidingID;
            try
            {
                if (fileName.EndsWith(".xlsx"))
                {
                    if (docentModel.XLSX(fileName))
                    {
                        docentModel.toDB();
                    }
                }
                else if (fileName.EndsWith(".xls"))
                {
                    if (docentModel.XLS(fileName))
                    {
                        docentModel.toDB();
                    }
                }
                else
                {
                    MessageBox.Show("File type not supported");
                }
            }
            catch
            {
                MessageBox.Show("Importing failed. Is the Excel sheet damaged or not safe?");
            }
        }

        public Boolean checkUrenLijst(string fileName)
        {
            Boolean passed = true;
            urenModel.PeriodeNaam = PeriodeNaam;
            urenModel.Start1 = start1;
            urenModel.End1 = end1;
            urenModel.Periode = Periode;
            urenModel.OpleidingID = OpleidingID;
            try
            {
                if (fileName.EndsWith(".xlsx"))
                {
                    if (urenModel.XLSX(fileName))
                    {
                        urenModel.toDB();
                    }
                }
                else if (fileName.EndsWith(".xls"))
                {
                    if (urenModel.XLS(fileName))
                    {
                        urenModel.toDB();
                    }
                }
                else
                {
                    passed = false;
                    MessageBox.Show("File type not supported");
                }
            }
            catch
            {
                passed = false;
                MessageBox.Show("Importing failed. Is the Excel sheet damaged or not safe?");
            }
            return passed;
        }

        public Boolean createPeriode()
        {
            DataTable test = DatabaseConnection.commandSelect("SELECT COUNT(*) FROM periodes WHERE periode = '" + Periode + "-" + PeriodeNaam + "'");
            if (Int32.Parse(test.Rows[0][0].ToString()) < 1)
            {
                DatabaseConnection.commandEdit("CALL procedure_create_periode('" + Periode + "-" + PeriodeNaam + "','" + Start1 + "','" + End1 + "');");
                return true;
            }
            else
            {
                return false;
            }
           
        }
    }
}
