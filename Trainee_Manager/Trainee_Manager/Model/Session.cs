using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee_Manager.Model
{
    public class Session
    {
        public string UserName { get; set; }
        public string Function { get; set; }
        public Boolean LoggedIn { get; set; }

        public void login(String username, String function)
        {
            UserName = username;
            Function = function;
            LoggedIn = true;
        }

        public void logout()
        {
            UserName = "";
            Function = "";
            LoggedIn = false;
        }
    }
}
