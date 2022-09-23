using MySql.Data.MySqlClient;
using Readit.Models.Entities;

namespace Readit.Models.DAO {
    public class CommentDAO {

        private readonly IConfiguration configuration;
        public DbContext dbContext { get; set; }
        private MemberDAO mDAO;
        private LinkDAO lDAO;

        public CommentDAO(IConfiguration _configuration) {
            this.configuration = _configuration;
            this.dbContext = new DbContext(this.configuration);
            this.mDAO = new MemberDAO(this.configuration);
            this.lDAO = new LinkDAO(this.configuration);

        }

        public bool AddComment(Comment comment) {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO Comment(Member_ID, Link_ID, Content, Publication_Date" +
                    "\r\nVALUES(@memberId, @linkId, @content, @publicationDate)");
                command.Parameters.Add(new MySqlParameter("@memberId", comment.User.ID));
                command.Parameters.Add(new MySqlParameter("@linkId", comment.Link.ID));
                command.Parameters.Add(new MySqlParameter("@content", comment.Content));
                command.Parameters.Add(new MySqlParameter("@publicationDate", comment.Publication));

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

        public IList<Comment> GetCommentsByLink(int linkId) {
            IList<Comment> comments = new List<Comment>();
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Comment WHERE Link_ID=@linkId", connection);
                command.Parameters.Add(new MySqlParameter("@linkId", linkId));
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read()) {
                    comments.Add(new Comment(
                            dataReader.GetInt32("ID"),
                            mDAO.GetMemberByID(dataReader.GetInt32("Member_ID")),
                            lDAO.GetLinkByID(dataReader.GetInt32("Link_ID")),
                            dataReader.GetString("Content"),
                            dataReader.GetDateTime("Publication_Date")
                    ));
                }

            } catch (Exception) {
                throw;
            } finally {
                connection.Close();
            }

            return comments;
        }
    }
}
