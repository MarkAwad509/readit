using MySql.Data.MySqlClient;
using Readit.Models.Entities;
using System.Data;
using System.Data.Common;

namespace ProjetWebServer_.DAL
{
    public class ConnexionDAO : IDAO
    {
        MySqlConnection conn;
        string myConnectionString = "server=mysql-projetwebserveur.alwaysdata.net;uid=279794_simuser;" + "pwd=SimpleUser12;database= ";
        public MySqlConnection connection()
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
            Member check = getUser(user);

            if(check.UserName == user && check.Password == pass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Member getUser( string user)
        {
            Member? current = null;
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
                current = new Member(reader.GetString("username"), reader.GetString("password"), reader.GetString("email"));
            }
            return current;
        }
    }
}
