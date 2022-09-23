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
                MySqlCommand command = new MySqlCommand("SELECT * FROM Link ORDER BY SUM(UpVote - DownVote) DESC", connection);
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

        public void GetPositiveVotesByLinkID(Link link) {
            int value;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT COUNT(ID) FROM Vote WHERE isUpVote=1 AND Link_ID=@id;", connection);
                command.Parameters.Add(new MySqlParameter("@ID", link.ID));
                value = Convert.ToInt32(command.ExecuteScalar());
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }
            link.UpVote = value;
        }

        public void GetNegativeVotesByLinkID(Link link) {
            int value;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT COUNT(ID) FROM Vote WHERE isUpVote=0 AND Link_ID=@id;", connection);
                command.Parameters.Add(new MySqlParameter("@ID", link.ID));
                value = Convert.ToInt32(command.ExecuteScalar());
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }
            link.DownVote = value;
        }

        public bool MemberHasVoted(Link link, Member member) {
            bool voted = true;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT IsUpVote FROM Vote WHERE Member_ID=@member_id AND Link_ID=@link_id", connection);
                command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.GetByte("IsUpvote") != 1 | reader.GetByte("IsUpvote") != 0)
                    voted = false;
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }
            return voted;
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

        public bool AddVote(Link link, Member member, bool isPositive) {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO Vote(Member_ID, Link_ID, IsUpVote)" +
                "\r\nVALUES (@member_id, @link_id, @ispositive)");
                command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                if (isPositive == true)
                    command.Parameters.Add(new MySqlParameter("@isUpVote", 1));
                else
                    command.Parameters.Add(new MySqlParameter("@isUpVote", 0));
                if (MemberHasVoted(link, member) == false) {
                    command.ExecuteNonQuery();
                    result = true;
                }
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }
            return result;
        }

        public bool updateVote(Member member, Link link) {
            bool found = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            MySqlCommand updateCommand;
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("Select IsUpVote FROM Vote WHERE Member_ID = @member_id AND Link_ID = @link_id", connection);
                command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                MySqlDataReader reader = command.ExecuteReader();
                int value = reader.GetInt32("IsUpVote");
                if (value == 0) {
                    updateCommand = new MySqlCommand("UPDATE Vote SET IsUpVote = 1 WHERE Member_ID = @member_id AND Link_ID = @link_id", connection);
                    command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                    command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                    found = true;
                }
                else {
                    updateCommand = new MySqlCommand("UPDATE Vote SET IsUpVote = 0 WHERE Member_ID = @member_id AND Link_ID = @link_id", connection);
                    command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                    command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                    found = true;
                }
            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }
            return found;
        }

        public bool DeleteVote(Member member, Link link) {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM Vote WHERE Member_ID=@member_id AND Link_ID=@link_id", connection);
                command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
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
