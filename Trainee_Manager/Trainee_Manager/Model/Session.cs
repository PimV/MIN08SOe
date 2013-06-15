using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainee_Manager.Controller;

namespace Trainee_Manager.Model
{
    public static class Session
    {
        public static string UserName { get; set; }
        public static string Function { get; set; }
        public static string ID { get; set; }               //ID as string because it can also be an email. 
        public static int DatabaseID { get; set; }          //ID van de student/docent zoals die in de database voorkomt.
        public static int CourseID { get; set; }            //Opleiding ID
        public static Boolean LoggedIn { get; set; }

        public static void login(String username, String function, string ID)
        {
            Session.UserName = username;
            Session.Function = function;
            Session.ID = ID;

            loadAccountInfo();

            Console.WriteLine("Session started, session information:");
            Console.WriteLine("username: " + UserName);
            Console.WriteLine("function: " + Function);
            Console.WriteLine("id: " + ID);
            Console.WriteLine("database-id: " + DatabaseID);
            Console.WriteLine("Course-id: " + CourseID);

            LoggedIn = true;
        }

        public static void loadAccountInfo()
        {
            DataTable dataTable = DatabaseConnection.commandSelect("CALL procedure_get_account_info('" + ID + "', '" + Function + "');");
            foreach (DataRow row in dataTable.Rows)
            {
                DatabaseID = Convert.ToInt32(row["id"]);
                CourseID = Convert.ToInt32(row["opleiding_id"]);
            }
        }

        public static void logout()
        {
            UserName = "";
            Function = "";
            ID = "";
            LoggedIn = false;
        }
    }
}
