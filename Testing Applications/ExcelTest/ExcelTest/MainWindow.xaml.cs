using Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using Excel = Microsoft.Office.Interop.Excel;


namespace ExcelTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //private Excel.Range range;
        //private Excel.Worksheet worksheet;
        //private Excel.Workbook workbook;
        //private Excel.Application excelApp;
        private static DataTable dt;
        private Thread myThread;
        private DataSet ds;

        public MainWindow()
        {
            InitializeComponent();



            //excelApp = new Excel.Application();

        }

        public delegate void showData(DataTable dt);

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog o = new OpenFileDialog();
                Nullable<bool> result = o.ShowDialog();
                if (result == true)
                {
                    if (o.FileName.EndsWith(".xlsx"))
                    {
                        myThread = new Thread(() =>
                        {
                            XLSX(o.FileName);
                        });
                        myThread.Start();
                        SplashWindow s = new SplashWindow(myThread);
                        s.ShowDialog();
                        Console.WriteLine(ds.Tables.Count);
                        this.DataContext = ds.Tables[0];


                    }
                    else if (o.FileName.EndsWith(".xls"))
                    {
                        myThread = new Thread(() =>
                        {
                            XLS(o.FileName);
                        });
                        myThread.Start();
                        SplashWindow s = new SplashWindow(myThread);
                        s.ShowDialog();
                        Console.WriteLine(ds.Tables.Count);
                        this.DataContext = ds.Tables[0];
                    }
                    else
                    {
                        MessageBox.Show("File type not supported");
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Something went wrong: " + exc.ToString());
            }
        }

        private void XLSX(string fileName)
        {
            try
            {
                FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                ds = excelReader.AsDataSet();
                excelReader.IsFirstRowAsColumnNames = true;

                foreach (DataTable table in ds.Tables)
                {
                    if (table.Rows.Count == 0) continue;
                    foreach (DataColumn dc in table.Columns)
                    {
                        string colName = table.Rows[0][dc.Ordinal].ToString().Trim();
                        if (!string.IsNullOrEmpty(colName) && !table.Columns.Contains(colName))
                        {
                            table.Columns[dc.Ordinal].ColumnName = colName;
                        }
                    }
                    table.Rows[0].Delete();
                }
                ds.AcceptChanges();

                excelReader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong: " + e.ToString());
            }
        }

        private void XLS(string fileName)
        {
            try
            {
                FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                ds = excelReader.AsDataSet();
                excelReader.IsFirstRowAsColumnNames = true;

                foreach (DataTable table in ds.Tables)
                {
                    if (table.Rows.Count == 0) continue;
                    foreach (DataColumn dc in table.Columns)
                    {
                        string colName = table.Rows[0][dc.Ordinal].ToString().Trim();
                        if (!string.IsNullOrEmpty(colName) && !table.Columns.Contains(colName))
                        {
                            table.Columns[dc.Ordinal].ColumnName = colName;
                        }
                    }
                    table.Rows[0].Delete();
                }
                ds.AcceptChanges();

                excelReader.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show("Something went wrong: " + e.ToString());
                
            }
        }
    }
}
