using MySqlConnector;
using projetwebserver.Models;
using System.Data;
using System.Data.Common;

namespace ProjetWebServer_.DAL
{
    public class ConnexionDAO : IDAO
    {
        MySqlConnection conn;
        string myConnectionString = "server=mysql-projetwebserveur.alwaysdata.net;uid=279794_simuser;" + "pwd=SimpleUser12;database=projetwebserveur_connexion ";
        public override MySqlConnection connection()
        {
            try
            {
                conn = new MySqlConnection(myConnectionString);
               
            }
            catch (MySqlException ex)
            {
                throw ex;
            }


            return conn;
        }

        public bool ConfirmPerson(string user, string pass)
        {
            User check = getUser(user);

            if(check.UserName == user && check.Password == pass)
            {
                return true;
            }
            else
            {
                return false;
            }

            
            return false;
        }
        public User getUser( string user)
        {
            User current= null;
           connection();
            MySqlCommand myCommand = conn.CreateCommand();
            myCommand.CommandText = "SELECT User from Users WHERE username= LIMIT 1" + user;
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                throw e;
                conn.Close();
            }
            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                current = new User(reader.GetString("username"), reader.GetString("password"), reader.GetString("email"), reader.GetString("name"), reader.GetString("phone"));
            }
            return current;
        }
    }
}
