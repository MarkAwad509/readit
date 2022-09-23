using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Readit.Models.Entities;
using System.Diagnostics.Metrics;
using System.Windows.Input;

namespace Readit.Models.DAO
{
    public class LinkDAO
    {
        private readonly IConfiguration configuration;
        public DbContext dbContext { get; set; }
        public MemberDAO dao;
        public LinkDAO(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.dbContext = new DbContext(configuration);
            dao = new MemberDAO(configuration);
        }

        public bool PostLink(Link link)
        {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO Link(Member_ID, Title, Description, UpVote, DownVote, Publication_Date)" +
                    "\r\nVALUES (@member_id, @title, @description, 0, 0, @publication_date)");
                command.Parameters.Add(new MySqlParameter("@member_id", link.Publisher.ID));
                command.Parameters.Add(new MySqlParameter("@title", link.Title));
                command.Parameters.Add(new MySqlParameter("@description", link.Description));
                command.Parameters.Add(new MySqlParameter("@publication_date", link.Publication));
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public bool UpdateLink(Link link)
        {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE Link SET Member_ID=@memberId,Title=@title,Description=@description,UpVote_Amount=@upvote,DownVote_Amount=@downvote,Publication_Date=@publicationDate  WHERE ID=@id", connection);
                command.Parameters.Add(new MySqlParameter("@id", link.ID));
                command.Parameters.Add(new MySqlParameter("@memberId", link.Publisher.ID));
                command.Parameters.Add(new MySqlParameter("@title", link.Title));
                command.Parameters.Add(new MySqlParameter("@description", link.Description));
                command.Parameters.Add(new MySqlParameter("@upvote", link.UpVote));
                command.Parameters.Add(new MySqlParameter("@downvote", link.DownVote));
                command.Parameters.Add(new MySqlParameter("@publicationDate", link.Publication));
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public bool DeleteLink(Link link)
        {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM Link WHERE ID=@id");
                command.Parameters.Add(new MySqlParameter("@id", link.ID));
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public IList<Link> GetLinksByMostPopular()
        {
            IList<Link> links = new List<Link>();
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Link ORDER BY SUM(UpVote - DownVote) DESC", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    links.Add(new Link()
                    {
                        ID = reader.GetInt32("ID"),
                        Publisher = dao.GetMemberByID(reader.GetInt32("Member_ID")),
                        Title = reader.GetString("Title"),
                        Description = reader.GetString("Description"),
                        UpVote = reader.GetInt32("UpVote"),
                        DownVote = reader.GetInt32("DownVote"),
                        Publication = reader.GetMySqlDateTime("Publication_Date")
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return links;
        }

        public IList<Link> GetLinksByMemberID(int id)
        {
            IList<Link> links = new List<Link>();
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Link WHERE MemberID", connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    links.Add(new Link()
                    {
                        ID = reader.GetInt32("ID"),
                        Publisher = dao.GetMemberByID(reader.GetInt32("Member_ID")),
                        Title = reader.GetString("Title"),
                        Description = reader.GetString("Description"),
                        Publication = reader.GetMySqlDateTime("Publication_Date")
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return links;
        }

        public void GetPositiveVotesByLinkID(Link link)
        {
            int value;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT COUNT(ID) FROM Vote WHERE isUpVote=1 AND Link_ID=@id;", connection);
                command.Parameters.Add(new MySqlParameter("@ID", link.ID));
                value = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            link.UpVote = value;
        }

        public void GetNegativeVotesByLinkID(Link link)
        {
            int value;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT COUNT(ID) FROM Vote WHERE isUpVote=0 AND Link_ID=@id;", connection);
                command.Parameters.Add(new MySqlParameter("@ID", link.ID));
                value = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            link.DownVote = value;
        }

        public bool MemberHasVoted(Link link, Member member)
        {
            bool voted = true;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT IsUpVote FROM Vote WHERE Member_ID=@member_id AND Link_ID=@link_id", connection);
                command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.GetByte("IsUpvote") != 1 | reader.GetByte("IsUpvote") != 0)
                    voted = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return voted;
        }

        public bool AddVote(Link link, Member member, bool isPositive)
        {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO Vote(Member_ID, Link_ID, IsUpVote)" +
                "\r\nVALUES (@member_id, @link_id, @ispositive)");
                command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                if (isPositive == true)
                    command.Parameters.Add(new MySqlParameter("@isUpVote", 1));
                else
                    command.Parameters.Add(new MySqlParameter("@isUpVote", 0));
                if (MemberHasVoted(link, member) == false)
                {
                    command.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public bool updateVote(Member member, Link link)
        {
            bool found = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            MySqlCommand updateCommand;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("Select IsUpVote FROM Vote WHERE Member_ID = @member_id AND Link_ID = @link_id", connection);
                command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                MySqlDataReader reader = command.ExecuteReader();
                int value = reader.GetInt32("IsUpVote");
                if (value == 0)
                {
                    updateCommand = new MySqlCommand("UPDATE Vote SET IsUpVote = 1 WHERE Member_ID = @member_id AND Link_ID = @link_id", connection);
                    command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                    command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                    found = true;
                }
                else
                {
                    updateCommand = new MySqlCommand("UPDATE Vote SET IsUpVote = 0 WHERE Member_ID = @member_id AND Link_ID = @link_id", connection);
                    command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                    command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                    found = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return found;
        }

        public bool DeleteVote(Member member, Link link)
        {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("DELETE FROM Vote WHERE Member_ID=@member_id AND Link_ID=@link_id", connection);
                command.Parameters.Add(new MySqlParameter("@member_id", member.ID));
                command.Parameters.Add(new MySqlParameter("@link_id", link.ID));
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public List<Comment> GetCommentsByLinkID(int id)
        {
            List<Comment> comments = new List<Comment>();
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Comment WHERE Link_ID=@id ORDER BY Publication_Date");
                command.Parameters.Add(new MySqlParameter("@id", id));
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comments.Add(new Comment()
                    {
                        ID = reader.GetInt32("ID"),
                        User = dao.GetMemberByID(reader.GetInt32("Member_ID")),
                        Content = reader.GetString("Content"),
                        Publication = reader.GetDateTime("Publication_Date")
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return comments;
        }
    }
}
