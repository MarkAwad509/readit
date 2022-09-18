using MySql.Data.MySqlClient;

namespace ProjetWebServer_.DAL
{
    public interface IDAO
    {
        public MySqlConnection connection();
    }
}
