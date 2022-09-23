using MySql.Data.Types;

namespace Readit.Models.Entities
{
    public class Link
    {
        public int ID { get; set; }
        public Member Publisher { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public MySqlDateTime Publication { get; set; }
        public int Score { get
            {
                return UpVote - DownVote;
            } 
        }
        public int VotesAmount { get
            {
                return UpVote + DownVote;
            } 
        }

        public IList<Comment> comments { get; set; }

        public Link() { }
    }
}
