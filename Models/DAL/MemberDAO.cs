using MySql.Data.MySqlClient;
using Readit.Models.Entities;
using System;
using Readit.Models.DAL;
namespace Readit.Models.DAO
{
    public class MemberDAO {
        private readonly IConfiguration configuration;
        public mproulx_5w6_readitContext dbContext { get; set; }

        public MemberDAO(IConfiguration _configuration) {
            this.configuration = _configuration;
            this.dbContext = new mproulx_5w6_readitContext();
        }

        public bool AddMember(Member member) {
           dbContext.Add<Member>(member);
            dbContext.SaveChanges();
            return true;
            /*bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO Member" +
                    "(Username, Email, Password)" +
                    "\r\nVALUES (@username, @email, @password);", connection);
                command.Parameters.Add(new MySqlParameter("@username", member.Username));
                command.Parameters.Add(new MySqlParameter("@email", member.Email));
                command.Parameters.Add(new MySqlParameter("@password", member.Password));
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    result = true;
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }
            return result;*/
        }

        public IList<Member> GetMembers() {
            return dbContext.Members.ToList();
            /*IList<Member> members = new List<Member>();
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Member", connection);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read()) {
                    members.Add(new Member() {
                        ID = dataReader.GetInt32("ID"),
                        Username = dataReader.GetString("Username"),
                        Email = dataReader.GetString("Email"),
                        Password = dataReader.GetString("Password")
                    }
                    );
                }
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }
            return members;*/
        }


        public Member GetMemberByEmail(string email) {

            return dbContext.Members.Where(e => e.Email == email).First();
            /*Member found;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Member WHERE Email=@email", connection);
                command.Parameters.Add(new MySqlParameter("@email", email));
                MySqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read()) {
                    found = new Member() {
                        ID = dataReader.GetInt32("ID"),
                        Username = dataReader.GetString("Username"),
                        Email = dataReader.GetString("Email"),
                        Password = dataReader.GetString("Password")
                    };
                }
                else {
                    found = null;
                }



            } catch (Exception) {
                throw;
            } finally {

                connection.Close();
            }
            return found;*/

        }

    }
}
