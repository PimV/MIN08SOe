using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trainee_Manager.Model;

namespace ExcelTest
{
    public class ExcelController
    {

        private ExcelStageModel stageModel;
        private ExcelDocentenModel docentModel;
        private ExcelUrenModel urenModel;

        private string periode1;
        public string Periode1 { get { return periode1; } set { periode1 = value; } }
        private string periode2;
        public string Periode2 { get { return periode2; } set { periode2 = value; } }

        public ExcelController()
        {
            stageModel = new ExcelStageModel();
            docentModel = new ExcelDocentenModel();
            urenModel = new ExcelUrenModel();
        }

        public void checkStageLijst(string fileName)
        {
            try
            {
                if (fileName.EndsWith(".xlsx"))
                {
                    stageModel.XLSX(fileName);
                }
                else if (fileName.EndsWith(".xls"))
                {
                    stageModel.XLS(fileName);
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

        public void checkDocentenLijst(string fileName)
        {
            try
            {
                if (fileName.EndsWith(".xlsx"))
                {
                    docentModel.XLSX(fileName);
                }
                else if (fileName.EndsWith(".xls"))
                {
                    docentModel.XLS(fileName);
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

        public void checkUrenLijst(string fileName)
        {
            urenModel.Periode1 = Periode1;
            urenModel.Periode2 = Periode2;
            try
            {
                if (fileName.EndsWith(".xlsx"))
                {
                    urenModel.XLSX(fileName);
                }
                else if (fileName.EndsWith(".xls"))
                {
                    urenModel.XLS(fileName);
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
    }
}
