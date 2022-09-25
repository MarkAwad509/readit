namespace Readit.Models.DAO
{
    public class DbContext
    {
        public string connectionString { get; set; }
        private readonly IConfiguration configuration;

        public DbContext(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.connectionString = configuration.GetConnectionString("Db");
        }
    }
}
