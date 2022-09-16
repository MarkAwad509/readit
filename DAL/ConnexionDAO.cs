using MySql.Data.MySqlClient;
using projetwebserver.Models;
using System.Data;
using System.Data.Common;

namespace ProjetWebServer_.DAL
{
    public class ConnexionDAO : IDAO
    {
        MySqlConnection conn;
        string myConnectionString = "server=mysql-projetwebserveur.alwaysdata.net;uid=279794_simuser;" + "pwd=SimpleUser12;database= ";
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
        }
        public User getUser( string user)
        {
            User? current = null;
           connection();
            MySqlCommand myCommand = conn.CreateCommand();
            myCommand.CommandText = "SELECT User from Users WHERE username="+user+" LIMIT 1";
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
                current = new User(reader.GetString("username"), reader.GetString("password"), reader.GetString("email"));
            }
            return current;
        }
    }
}
