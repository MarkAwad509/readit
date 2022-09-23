using MySql.Data.MySqlClient;
using Readit.Models.Entities;

namespace Readit.Models.DAO
{
    public class LinkDAO
    {
        private readonly IConfiguration configuration;
        public DbContext dbContext { get; set; }
        private MemberDAO mDAO;


        public LinkDAO(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.dbContext = new DbContext(this.configuration);
            this.mDAO = new MemberDAO(this.configuration);
        }

        public bool AddLink(Link link){
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try{
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO Link(Member_ID, Title, Description, UpVote_Amount, DownVote_Amount, Publication_Date" +
                    "\r\nVALUES (@memberId, @title, @description, @upvote, @downvote, @publicationDate)");
                command.Parameters.Add(new MySqlParameter("@memberId", link.Publisher.ID));
                command.Parameters.Add(new MySqlParameter("@title", link.Title));
                command.Parameters.Add(new MySqlParameter("@description", link.Description));
                command.Parameters.Add(new MySqlParameter("@upvote", link.UpVote));
                command.Parameters.Add(new MySqlParameter("@downvote", link.DownVote));
                command.Parameters.Add(new MySqlParameter("@publicationDate", link.PublicationDate));

                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    result = true;
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }
            return result;
        }

        public Link GetLinkByID(int linkId) {
            Link? link = null;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Link WHERE ID=@id", connection);
                command.Parameters.Add(new MySqlParameter("@id", linkId));
                MySqlDataReader dataReader = command.ExecuteReader();
                link = new Link(
                        dataReader.GetInt32("ID"),
                        mDAO.GetMemberByID(dataReader.GetInt32("Member_ID")),
                        dataReader.GetString("Title"),
                        dataReader.GetString("Description"),
                        dataReader.GetInt32("UpVote_Amount"),
                        dataReader.GetInt32("DownVote_Amount"),
                        dataReader.GetDateTime("Publication_Date")
                    );
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }
            return link;
        }

        public IList<Link> GetLinksSortedByScore() {
            IList<Link> links = new List<Link>();
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Link", connection);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read()) {
                    links.Add(new Link(
                        dataReader.GetInt32("ID"),
                        mDAO.GetMemberByID(dataReader.GetInt32("Member_ID")),
                        dataReader.GetString("Title"),
                        dataReader.GetString("Description"),
                        dataReader.GetInt32("UpVote_Amount"),
                        dataReader.GetInt32("DownVote_Amount"),
                        dataReader.GetDateTime("Publication_Date")
                    ));
                }
            } catch (Exception) {
                throw;
            } finally {
                links.OrderBy(l => l.Score);
                connection.Close();
            }

            return links;
        }

        public IList<Link> GetLinksByMember(int memberId) {
            IList<Link> links = new List<Link>();
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Link WHERE Member_ID=@memberId", connection);
                command.Parameters.Add(new MySqlParameter("@memberId", memberId));
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read()) {
                    links.Add(new Link(
                        dataReader.GetInt32("ID"),
                        mDAO.GetMemberByID(dataReader.GetInt32("Member_ID")),
                        dataReader.GetString("Title"),
                        dataReader.GetString("Description"),
                        dataReader.GetInt32("UpVote_Amount"),
                        dataReader.GetInt32("DownVote_Amount"),
                        dataReader.GetDateTime("Publication_Date")
                    ));

                }
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }

            return links;
        }

        public bool UpdateLink(Link link) {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE Link SET Member_ID=@memberId,Title=@title,Description=@description,UpVote_Amount=@upvote,DownVote_Amount=@downvote,Publication_Date=@publicationDate  WHERE ID=@id", connection);
                command.Parameters.Add(new MySqlParameter("@id", link.ID));
                command.Parameters.Add(new MySqlParameter("@memberId", link.Publisher.ID));
                command.Parameters.Add(new MySqlParameter("@title", link.Title));
                command.Parameters.Add(new MySqlParameter("@description", link.Description));
                command.Parameters.Add(new MySqlParameter("@upvote", link.UpVote));
                command.Parameters.Add(new MySqlParameter("@downvote", link.DownVote));
                command.Parameters.Add(new MySqlParameter("@publicationDate", link.PublicationDate));
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    result = true;
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }

            return result;
        }

        public bool DeleteLink(Link link) {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM Link WHERE ID=@id");
                command.Parameters.Add(new MySqlParameter("@id", link.ID));

                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    result = true;
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }

            return result;
        }
    }
}
