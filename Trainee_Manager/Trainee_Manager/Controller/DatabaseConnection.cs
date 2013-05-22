using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Trainee_Manager.Controller
{
    class DatabaseConnection
    {
        private MySqlConnection connection;
        private MySqlCommand cmd;
        private MySqlDataReader reader;

        //Initialize connection information
        public void initializeConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            connection = new MySqlConnection(connString);
        }

        //Open connection to database
        public Boolean openConnection()
        {
            try
            {
                //If the connection aint open yet, open it
                if (!checkConnection())
                {
                    connection.Open();
                    return true;
                }
                //If the connection is already open, do not open it again
                else
                {
                    MessageBox.Show("De verbinding met de database is al geopend.");
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                //The two most common error numbers when connecting:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Er kan geen verbinding worden gemaakt met de database server. Neem contact op met uw administrator.");
                        return false;
                    case 1045:
                        MessageBox.Show("De gebruikersnaam of wachtwoord is niet correct.");
                        return false;
                    default:
                        MessageBox.Show("Er kan geen verbinding worden gemaakt met de database server. Neem contact op met uw administrator.");
                        return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foutmelding: " + e + " Kan geen verbinding maken met de database");
                return false;
            }
        }

        //close connection to database
        public Boolean closeConnection()
        {
            try
            {
                //If the connection is open, close it
                if (checkConnection())
                {
                    connection.Close();
                    return true;
                }
                //If the connection aint open, you cannot close it
                else
                {
                    MessageBox.Show("De verbinding met de database is al gesloten.");
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foutmelding: " + e + " De verbinding kan niet worden gesloten.");
                return false;
            }
        }

        //Check if the connection is open, return true or false
        public Boolean checkConnection()
        {
            return connection.Ping();
        }

        //Check of the login credentials are valid
        public Boolean chechLoginCredentials(String username, String password)
        {
            try
            {
                //Open the connection to the database
                openConnection();

                if (checkConnection())
                {
                    cmd = new MySqlCommand("SELECT * FROM login WHERE gebruiker = '" + username + "' AND wachtwoord = '" + password + "';");
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();

                    if (reader.Read() != false)
                    {
                        if (reader.IsDBNull(0) == true)
                        {
                            disposeAfterLoginTry();
                            return false;
                        }
                        else
                        {
                            disposeAfterLoginTry();
                            return true;
                        }
                    }
                    else
                    {
                        disposeAfterLoginTry();
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Foutmelding: " + e);
                return false;
            }
        }

        //Close the command connection / dispose the reader / disopse the command and close the connection
        private void disposeAfterLoginTry()
        {
            reader.Dispose();
            cmd.Dispose();
            closeConnection();
        }
    }
}
