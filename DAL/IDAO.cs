using MySql.Data.MySqlClient;


namespace ProjetWebServer_.DAL
{
    public abstract class IDAO
    {

        public abstract MySqlConnection connection();

    }
}
