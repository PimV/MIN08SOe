using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee_Manager.Model
{
    public static class Session
    {
        public static string UserName { get; set; }
        public static string Function { get; set; }
        public static string ID { get; set; }              //ID as string because it can also be an email. 
        public static Boolean LoggedIn { get; set; }

        public static void login(String username, String function, string ID)
        {
            Session.UserName = username;
            Session.Function = function;
            Session.ID = ID;
            LoggedIn = true;
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
