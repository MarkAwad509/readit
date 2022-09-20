using MySql.Data.MySqlClient;
using Readit.Models.Entities;

namespace Readit.Models.DAO
{
    public class LinkDAO
    {
        private readonly IConfiguration configuration;
        public DbContext dbContext { get; set; }

        public LinkDAO(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.dbContext = new DbContext(configuration);
        }

        public bool addLink(Link link)
        {
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO Link()")
            }
        }
    }
}
