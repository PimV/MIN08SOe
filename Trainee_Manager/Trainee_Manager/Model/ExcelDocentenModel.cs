using Excel;
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
    public class ExcelDocentenModel
    {
        private static DataTable dt;
        private DataSet ds;
        private List<string> columnNames;
        private List<string> correctColNames;

        public ExcelDocentenModel()
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
                List<string> queryFeed = new List<string>();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.Ordinal == 0)
                    {
                        if (dr[dc.ColumnName].ToString() == string.Empty)
                        {
                            return false;
                        }
                        else
                        {

                        }

                    }

                    queryFeed.Add(dr[dc.ColumnName].ToString());
                }
                if (queryFeed.Count > 0)
                {
                    queryFeed[6] = queryFeed[6].Replace(" ", ""); //Filters " " from Postcode



                    DataTable datatable = DatabaseConnection.commandSelect("CALL procedure_docent_details_import('" + queryFeed[0] + "','" + queryFeed[1] + "','" + queryFeed[2] + "','" + queryFeed[3] + "','" + queryFeed[4] + "','" + queryFeed[5] + "','" + queryFeed[6].Trim() + "','" + queryFeed[7] + "','" + queryFeed[8].Trim() + "');");

                }
            }
            return true;
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

            correctColNames.Add("Naam");
            correctColNames.Add("E-mail");
            correctColNames.Add("Kamernr");
            correctColNames.Add("Telnr");
            correctColNames.Add("Naam volledig");
            correctColNames.Add("Adres");
            correctColNames.Add("Postcode");
            correctColNames.Add("Plaats");
            correctColNames.Add("Telnr privé");
            correctColNames.Add("M");
            correctColNames.Add("");
            correctColNames.Add("D");
            correctColNames.Add("");
            correctColNames.Add("W");
            correctColNames.Add("");
            correctColNames.Add("D");
            correctColNames.Add("");
            correctColNames.Add("V");
            correctColNames.Add("");
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
                    string colName = dt.Rows[1][dc.Ordinal].ToString().Trim();
                    Console.WriteLine(colName);
                    columnNames.Add(colName);
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
                    string colName = dt.Rows[1][dc.Ordinal].ToString().Trim();
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

    }
}
