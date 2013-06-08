using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
using Trainee_Manager.Controller;
using Trainee_Manager.Model;

namespace Trainee_Manager.View
{
    /// <summary>
    /// Interaction logic for TraineeDetails.xaml
    /// </summary>
    public partial class TraineeDetails : UserControl
    {
        private int id;

        private static DataTable dataTable;
        private KoppelController _ratingController;
        private MainWindow mainWindow;

        public TraineeDetails()
        { }

        public TraineeDetails(MainWindow mainWindow, int id, KoppelController _ratingController)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            this._ratingController = _ratingController;

            //Id represents the id in the stage table
            this.id = id;
            Console.WriteLine(id);
            getData();
            showTop5(_ratingController.Calc.Docenten.DocentenList);
        }

        private void getData()
        {

            //Call the procedure to load the mysql data
            dataTable = DatabaseConnection.commandSelect("CALL procedure_stage_details(" + id + ");");

            foreach (DataRow row in dataTable.Rows)
            {
                label_studentnummer.Content = row["studentnummer"].ToString();
                label_studentnaam.Content = row["student"].ToString();
                label_studentemail.Content = row["email"].ToString();
                label_studentnummer2.Content = row["studentnummer2"].ToString();
                label_studentnaam2.Content = row["student2"].ToString();
                label_studentemail2.Content = row["email2"].ToString();
                label_begeleider.Content = row["begeleider"].ToString();
                label_lezer.Content = row["lezer"].ToString();
                textblock_onderwerpen.Text = row["onderwerpen"].ToString();
                label_bedrijf.Content = row["bedrijf"].ToString();
                label_bedrijfsplaats.Content = row["locatie"].ToString();
                label_bedrijfsadres.Content = row["adres"].ToString();
                label_bedrijfssite.Content = row["website"].ToString();
                textblock_opmerking.Text = row["opmerking"].ToString();
                label_bedrijfsbegeleider.Content = row["bedrijfsbegeleider"].ToString();
                label_bedrijfsbegeleideremail.Content = row["bedrijfsbegeleider_email"].ToString();
                label_bedrijfsbegeleidertel.Content = row["bedrijfsbegeleider_tel"].ToString();
                textblock_opdracht.Text = row["opdracht"].ToString();

                //ja of nee ipv true or false
                if (row["afstudeerstage"].ToString().Equals("True"))
                {
                    label_afstudeerstage.Content = "Ja";
                }
                else
                {
                    label_afstudeerstage.Content = "Nee";
                }

                //ja of nee ipv true or false
                if (row["toestemming"].ToString().Equals("True"))
                {
                    label_toestemming.Content = "Ja";
                }
                else
                {
                    label_toestemming.Content = "Nee";
                }

                //ja of nee ipv true or false
                if (row["goedkeuring"].ToString().Equals("True"))
                {
                    label_goedkeuring.Content = "Ja";
                }
                else
                {
                    label_goedkeuring.Content = "Nee";
                }

                if (row["EPS"].ToString().Equals("True"))
                {
                    epsBox.IsChecked = true;
                }
                else
                {
                    epsBox.IsChecked = false;
                }
            }
        }

        public void showTop5(List<Docent> docenten)
        {
            //  DocentList list = new DocentList();

            if (label_begeleider.Content == string.Empty && epsBox.IsChecked == false)
            {
                for (int i = 0; i < docenten.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            vk1_naam.Content = docenten.ElementAt(i).Naam;
                            string kenmerken1 = "";
                            foreach (string s in docenten.ElementAt(i).kenmerkenlijst)
                            {
                                kenmerken1 += s + ",";
                            }
                            vk1_kennis.Content = kenmerken1;
                            vk1_afstand.Content = docenten.ElementAt(i).Afstand;
                            //vk1_relatie.Content = "2";
                            break;
                        case 1:
                            vk2_naam.Content = docenten.ElementAt(i).Naam;
                            string kenmerken2 = "";
                            foreach (string s in docenten.ElementAt(i).kenmerkenlijst)
                            {
                                kenmerken2 += s + ",";
                            }
                            vk2_kennis.Content = kenmerken2;
                            vk2_afstand.Content = docenten.ElementAt(i).Afstand;
                            //vk2_relatie.Content = "0";
                            break;
                        case 2:
                            vk3_naam.Content = docenten.ElementAt(i).Naam;
                            string kenmerken3 = "";
                            foreach (string s in docenten.ElementAt(i).kenmerkenlijst)
                            {
                                kenmerken3 += s + ",";
                            }
                            vk3_kennis.Content = kenmerken3;
                            vk3_afstand.Content = docenten.ElementAt(i).Afstand;
                            //vk3_relatie.Content = "2";
                            break;
                        case 3:
                            vk4_naam.Content = docenten.ElementAt(i).Naam;
                            string kenmerken4 = "";
                            foreach (string s in docenten.ElementAt(i).kenmerkenlijst)
                            {
                                kenmerken4 += s + ",";
                            }
                            vk4_kennis.Content = kenmerken4;
                            vk4_afstand.Content = docenten.ElementAt(i).Afstand;
                            //vk4_relatie.Content = "1";
                            break;

                        case 4:
                            vk5_naam.Content = docenten.ElementAt(i).Naam;
                            string kenmerken5 = "";
                            foreach (string s in docenten.ElementAt(i).kenmerkenlijst)
                            {
                                kenmerken5 += s + ",";
                            }
                            vk5_kennis.Content = kenmerken5;
                            vk5_afstand.Content = docenten.ElementAt(i).Afstand;
                            //vk5_relatie.Content = "0";
                            break;
                    }
                }
            }
            else
            {
                vkgrid.Visibility = Visibility.Collapsed;

                //vk1.Visibility = Visibility.Collapsed;
                //vk2.Visibility = Visibility.Collapsed;
                //vk3.Visibility = Visibility.Collapsed;
                //vk4.Visibility = Visibility.Collapsed;
                //vk5.Visibility = Visibility.Collapsed;

                top5label.Visibility = Visibility.Collapsed;
                //vk1_naam.Content = "";
                //vk2_naam.Content = "";
                //vk3_naam.Content = "";
                //vk4_naam.Content = "";
                //vk5_naam.Content = "";

                //vk1_kennis.Content = "";
                //vk2_kennis.Content = "";
                //vk3_kennis.Content = "";
                //vk4_kennis.Content = "";
                //vk5_kennis.Content = "";

                //vk1_relatie.Content = "";
                //vk2_relatie.Content = "";
                //vk3_relatie.Content = "";
                //vk4_relatie.Content = "";
                //vk5_relatie.Content = "";

                //vk1_afstand.Content = "";
                //vk2_afstand.Content = "";
                //vk3_afstand.Content = "";
                //vk4_afstand.Content = "";
                //vk5_afstand.Content = "";

                //koppel1.IsEnabled = false;
                //koppel2.IsEnabled = false;
                //koppel3.IsEnabled = false;
                //koppel4.IsEnabled = false;
                //koppel5.IsEnabled = false;
            }
        }

        private void koppel_Click(object sender, RoutedEventArgs e)
        {
            
            string koppelKnopNaam = (sender as Button).Name;

            int koppelDocID = -1;
            int selectedTop5Index = -1;

            if (koppelKnopNaam.Contains('1'))
            {
                koppelDocID = _ratingController.Calc.Docenten.DocentenList[0].Id;
               // _ratingController.KoppelDocent(koppelDocID, id);
                selectedTop5Index = 0;
                
            }
            else if (koppelKnopNaam.Contains('2'))
            {
                koppelDocID = _ratingController.Calc.Docenten.DocentenList[1].Id;
               // _ratingController.KoppelDocent(koppelDocID, id);
                selectedTop5Index = 1;
            }
            else if (koppelKnopNaam.Contains('3'))
            {
                koppelDocID = _ratingController.Calc.Docenten.DocentenList[2].Id;
               // _ratingController.KoppelDocent(koppelDocID, id);
                selectedTop5Index = 2;
            }
            else if (koppelKnopNaam.Contains('4'))
            {
                koppelDocID = _ratingController.Calc.Docenten.DocentenList[3].Id;
               // _ratingController.KoppelDocent(koppelDocID, id);
                selectedTop5Index = 3;
            }
            else if (koppelKnopNaam.Contains('5'))
            {
                koppelDocID = _ratingController.Calc.Docenten.DocentenList[4].Id;
                selectedTop5Index = 4;
            }
            
            _ratingController.KoppelDocent(koppelDocID, id);
           
            mainWindow.showTraineeDetailsScreen();

            MessageBox.Show("U heeft " + _ratingController.Calc.Docenten.DocentenList[selectedTop5Index].Naam + " gekoppeld aan deze stage!");
            
            
        }
    }
}
