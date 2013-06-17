using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using Trainee_Manager.Helper_Classes;
using Trainee_Manager.Model;
using Trainee_Manager.View;

namespace Trainee_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Login2 login;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Utilities.UnprotectConnectionString();

            login = new Login2();
            login.Show();

            //RatingGegevensImporter r = new RatingGegevensImporter();
            //r.CreateDocenten();
            //r.CreateOpdrachten();


            


        }

        protected override void OnExit(ExitEventArgs e)
        {
            Utilities.ProtectConnectionString();
            base.OnExit(e);

        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }



    }
}
