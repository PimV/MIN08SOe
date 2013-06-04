﻿using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trainee_Manager.Controller;

namespace Trainee_Manager.Model
{
    public class ExcelUrenModel
    {

        private static DataTable dt;
        private DataSet ds;
        private List<string> columnNames;
        private List<string> correctColNames;

        private string periode1;
        public string Periode1 { get { return periode1; } set { periode1 = value; } }

        private string periode2;
        public string Periode2 { get { return periode2; } set { periode2 = value; } }

        public ExcelUrenModel()
        {
            columnNames = new List<string>();
            fillCorrectColumnNames();


        }

        private Boolean toDB()
        {
            DatabaseConnection.initializeConnection();
            dt.Rows.RemoveAt(0);
            dt.AcceptChanges();
            dt.Rows.RemoveAt(0);
            dt.AcceptChanges();
            Console.WriteLine(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr != dt.Rows[0] && dr != dt.Rows[dt.Rows.Count - 1])
                {
                    List<string> queryFeed = new List<string>();
                    foreach (DataColumn dc in dt.Columns)
                    {

                        string name = "";
                        if (dc.Ordinal == 0)
                        {
                            if (dr[dc.ColumnName].ToString() == string.Empty)
                            {
                                return false;

                            }
                            else
                            {
                                if (dr[dc.ColumnName].ToString().StartsWith("_"))
                                {
                                    break;
                                }
                                else if (dr[dc.ColumnName].ToString().Contains(','))
                                {
                                    string[] nameParts = dr[dc.ColumnName].ToString().Split(',');
                                    name = nameParts[1].Trim() + " " + nameParts[0].Trim();
                                    Console.WriteLine(name);
                                }
                                else
                                {
                                    name = dr[dc.ColumnName].ToString();
                                }
                            }

                        }
                        if (dt.Rows[0][dc.ColumnName].ToString().Equals("rest-tm2") || dt.Rows[0][dc.ColumnName].ToString().Equals("rest"))
                        {
                            queryFeed.Add(dr[dc.ColumnName].ToString());
                        }
                        else if (dt.Rows[0][dc.ColumnName].ToString().Equals("Docent"))
                        {
                            queryFeed.Add(name);
                        }

                    }
                    if (queryFeed.Count > 0)
                    {
                        Console.WriteLine("QF2: " + queryFeed[2]);
                        int time1 = Int32.Parse(queryFeed[1]);
                        int time2 = Int32.Parse(queryFeed[2]) - time1;
                        string periode1 = "";
                        string periode2 = "";
                        DataTable datatable = DatabaseConnection.commandSelect("CALL procedure_uren_import('" + queryFeed[0] + "','" + time1 + ",'" + periode1 + "'," + time2 + ",'" + periode2 + ");");

                    }
                }
            }
            return true;
        }

        public void XLSX(string fileName)
        {
            try
            {
                FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                ds = excelReader.AsDataSet();
                dt = ds.Tables[0];

                foreach (DataColumn dc in dt.Columns)
                {
                    string colName = dt.Rows[2][dc.Ordinal].ToString().Trim();
                    columnNames.Add(colName);
                    Console.WriteLine(colName);
                }
                ds.AcceptChanges();
                Console.WriteLine(columnNames.Count);
                if (checkColumns())
                {
                    toDB();
                }
                excelReader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong: " + e.ToString());
            }
        }

        public void XLS(string fileName)
        {
            try
            {
                FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                ds = excelReader.AsDataSet();
                dt = ds.Tables[0];

                foreach (DataColumn dc in dt.Columns)
                {
                    string colName = dt.Rows[2][dc.Ordinal].ToString().Trim();
                    Console.WriteLine(colName);
                    columnNames.Add(colName);
                }
                ds.AcceptChanges();
                if (checkColumns())
                {
                    toDB();
                }
                excelReader.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("Something went wrong: " + e.ToString());

            }
        }

        private Boolean checkColumns()
        {
            Boolean correct = true;
            foreach (string s in correctColNames)
            {
                if (!s.Equals(columnNames[correctColNames.IndexOf(s)]))
                {
                    MessageBox.Show("Format komt niet overeen");
                    correct = false;
                    break;
                }

            }
            return correct;
        }

        private void fillCorrectColumnNames()
        {
            correctColNames = new List<string>();

            correctColNames.Add("Docent");
            correctColNames.Add("Opl.");
            correctColNames.Add("fte");
            correctColNames.Add("cu1");
            correctColNames.Add("dbu1");
            correctColNames.Add("rest1");
            correctColNames.Add("cu2");
            correctColNames.Add("dbu2");
            correctColNames.Add("rest2");
            correctColNames.Add("rest-tm2");

            correctColNames.Add("cu3");
            correctColNames.Add("dbu3");
            correctColNames.Add("rest3");
            correctColNames.Add("rest-tm3");
            correctColNames.Add("cu4");
            correctColNames.Add("dbu4");
            correctColNames.Add("rest4");
            correctColNames.Add("totaal");
            correctColNames.Add("rest");
        }

    }
}