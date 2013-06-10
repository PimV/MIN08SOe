using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Windows;

namespace Trainee_Manager.Controller
{
    public static class ExportToExcel
    {
        public static void exportDataTable(DataTable dataTable)
        {
            SaveFileDialog o = new SaveFileDialog();
            o.CreatePrompt = true;
            o.AddExtension = true;
            o.OverwritePrompt = true;         
            o.Filter = "Excel File (2007>) (*.xlsx)|*.xlsx|Excel File (<2006) (*.xls)|*.xls";
            Nullable<bool> result = o.ShowDialog();
            if (result == true)
            {
                string filePath = o.FileName;

                FileInfo fi = new FileInfo(filePath);

                DataSet data = new DataSet();

                data.Tables.Clear();
                
                data.Tables.Add(dataTable);

                ExcelPackage pck = new ExcelPackage(fi);

                if (pck.File.Exists)
                {
                    pck.File.Delete();                    
                    pck = new ExcelPackage(fi);                    
                }
                //Add the Content sheet
                var ws = pck.Workbook.Worksheets.Add("Content");
                ws.Cells["A1"].LoadFromDataTable(dataTable, true);
                pck.Save();    

               
            }
                   
        }

        /*
        public static void export(DataTable dataTable)
        {
            Microsoft.Office.Interop.Excel.Application excel = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;

            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;
            Microsoft.Office.Interop.Excel.Range rng = null;
            
            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                wb = excel.Workbooks.Add();
                ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

                for (int Idx = 0; Idx < dataTable.Columns.Count; Idx++)
                {
                    ws.Range["A1"].Offset[0, Idx].Value = dataTable.Columns[Idx].ColumnName;
                }

                for (int Idx = 0; Idx < dataTable.Rows.Count; Idx++)
                {
                    ws.Range["A2"].Offset[Idx].Resize[1, dataTable.Columns.Count].Value =
                    dataTable.Rows[Idx].ItemArray;
                }

                excel.Visible = true;
                wb.Activate();
            }
            catch (COMException ex)
            {
                MessageBox.Show("Error accessing Excel: " + ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }*/
    }
}
