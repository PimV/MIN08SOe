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
    public class ExcelStageModel
    {
        private static DataTable dt;
        private DataSet ds;
        private List<string> columnNames;
        private List<string> correctColNames;

        public ExcelStageModel()
        {
            columnNames = new List<string>();
            fillCorrectColumnNames();
        }

        private void toDB()
        {
            DatabaseConnection.initializeConnection();
            dt.Rows.RemoveAt(0);
            foreach (DataRow dr in dt.Rows)
            {
                List<string> queryFeed = new List<string>();
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.Ordinal == 0)
                    {
                        int succeeded;
                        Int32.TryParse(dr[dc.ColumnName].ToString(), out succeeded);
                        if (succeeded == 0)
                        {
                            break;
                        }
                        else
                        {

                        }

                    }
                    queryFeed.Add(dr[dc.ColumnName].ToString());
                }
                if (queryFeed.Count > 0)
                {
                    queryFeed[9] = queryFeed[9].Replace("'", ""); //Filters \'\ from Plaats
                    queryFeed[8] = queryFeed[8].Replace(" ", ""); //Filters " " from Postcode

                    int result;
                    Int32.TryParse(queryFeed[6], out result); //Parses string from Huisnummer to INT


                    DatabaseConnection.commandEdit("CALL procedure_student_details_import(" + Int32.Parse(queryFeed[0]) + ",'" + queryFeed[1] + "','" + queryFeed[2] + "','" + queryFeed[3] + "','" + queryFeed[4] + "','" + queryFeed[5] + "','" + result + "','" + queryFeed[7] + "','" + queryFeed[8].Trim() + "','" + queryFeed[9] + "','" + queryFeed[10] + "','" + queryFeed[11] + "');");
                }
            }
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
                    string colName = dt.Rows[0][dc.Ordinal].ToString().Trim();
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
                    string colName = dt.Rows[0][dc.Ordinal].ToString().Trim();
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

            correctColNames.Add("Studentnr.");
            correctColNames.Add("Achternaam");
            correctColNames.Add("Voorvoegsels");
            correctColNames.Add("Roepnaam");
            correctColNames.Add("E-mail adres");
            correctColNames.Add("Straatnaam");
            correctColNames.Add("Nummer");
            correctColNames.Add("Toevoeging nummer");
            correctColNames.Add("Postcode");
            correctColNames.Add("Plaats");

            correctColNames.Add("Telefoonnummer");
            correctColNames.Add("SLB6-1");
            correctColNames.Add("SLB6-2");
            correctColNames.Add("SLB6-3");
            correctColNames.Add("SLB6-T");
            correctColNames.Add("SLB7-1");
            correctColNames.Add("SLB7-2");
            correctColNames.Add("SLB7-T");
            correctColNames.Add("EC's");
            correctColNames.Add("P");

            correctColNames.Add("EPS");
            correctColNames.Add("Form. goedkeuring");
            correctColNames.Add("Toest. Ex. Cie.");
            correctColNames.Add("Stage- contract");
            correctColNames.Add("Begeleidend docent");
            correctColNames.Add("Bijzonderheden");
            correctColNames.Add("Bedrijf");
            correctColNames.Add("Branche");
            correctColNames.Add("Straat");
            correctColNames.Add("Nr.");

            correctColNames.Add("Toevoeging");
            correctColNames.Add("Postcode");
            correctColNames.Add("Plaats");
            correctColNames.Add("Land");
            correctColNames.Add("Bedrijfsbegeleider");
            correctColNames.Add("E-mail");
            correctColNames.Add("Tel. nr Bedrijf");
            correctColNames.Add("Tel. nr Begeleider");
            correctColNames.Add("Website stageadres");
        }


    }
}
