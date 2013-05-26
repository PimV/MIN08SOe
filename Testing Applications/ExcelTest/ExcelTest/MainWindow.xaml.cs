using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using Excel = Microsoft.Office.Interop.Excel;


namespace ExcelTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Excel.Range range;
        private Excel.Worksheet worksheet;
        private Excel.Workbook workbook;
        private Excel.Application excelApp;
        private static DataTable dt;
        private Thread myThread;

        public MainWindow()
        {
            InitializeComponent();



            excelApp = new Excel.Application();

        }

        public delegate void showData(DataTable dt);

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            Nullable<bool> result = o.ShowDialog();
            if (result == true)
            {
                if (o.FileName.EndsWith(".xlsx") || o.FileName.EndsWith(".xls"))
                {
                    workbook = excelApp.Workbooks.Open(o.FileName);
                    myThread = new Thread(() =>
                    {
                        getData();
                    });
                    myThread.Start();
                    SplashWindow s = new SplashWindow(myThread);
                    s.ShowDialog();
                    this.DataContext = dt.DefaultView;

                }
            }
        }

        private void getData()
        {

            worksheet = (Excel.Worksheet)workbook.Sheets.get_Item(1);

            int row = 0;
            int column = 0;

            range = worksheet.UsedRange;
            dt = new DataTable();

            for (row = 1; row <= range.Rows.Count; row++)
            {
                DataRow dr = null;
                if (row == 1)
                {

                }
                else
                {
                    dr = dt.NewRow();
                }
                for (column = 1; column <= range.Columns.Count; column++)
                {
                    if (row == 1)
                    {
                        if ((range.Cells[row, column] as Excel.Range).Value2 != null)
                            dt.Columns.Add((range.Cells[row, column] as Excel.Range).Value2.ToString());
                    }
                    else
                    {
                        if ((range.Cells[row, column] as Excel.Range).Value2 != null)
                        {
                            dr[column - 1] = (range.Cells[row, column] as Excel.Range).Value2.ToString();
                        }
                    }
                }
                if (dr != null)
                {
                    dt.Rows.Add(dr);
                }
            }
            workbook.Close(false, Missing.Value, Missing.Value);
            excelApp.Quit();

        }
    }
}
