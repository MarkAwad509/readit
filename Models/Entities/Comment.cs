using MySql.Data.Types;

namespace Readit.Models.Entities
{
    public class Comment
    {
        public int ID { get; set; }
        public Member User { get; set; }
        public string Content { get; set; }
        public MySqlDateTime Publication { get; set; }

        public Comment() { }

        public Comment(int iD, Member user, string content, MySqlDateTime publication)
        {
            this.ID = iD;
            this.User = user;
            this.Content = content;
            this.Publication = publication;
        }
    }
}
