using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Trainee_Manager.Controller
{
    public static class ExportToExcel
    {
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
        }
    }
}
