namespace Readit.Models.Entities
{
    public class Link
    {
        private static int AUTO_ID = -1;
        public int ID { get; set; }
        public Member Publisher { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Score { get { return UpVote - DownVote; } }
        public int VotesAmount { get { return UpVote + DownVote; } }
        public IList<Comment> Comments { get; set; }

        public Link() { }

        public Link(int ID, Member publisher, string title, string description, int upVote, int downVote, DateTime date, IList<Comment> comments) {
            this.ID = ID;
            Publisher = publisher;
            Title = title;
            Description = description;
            UpVote = upVote;
            DownVote = downVote;
            PublicationDate = date;
            this.Comments = comments;
        }

        public Link(int ID, Member publisher, string title, string description, int upVote, int downVote, DateTime date) {
            this.ID = ID;
            Publisher = publisher;
            Title = title;
            Description = description;
            UpVote = upVote;
            DownVote = downVote;
            PublicationDate = date;
        }

        public Link(Member publisher, string title, string description, int upVote, int downVote, DateTime date, IList<Comment> comments) {
            ID = AUTO_ID++;
            Publisher = publisher;
            Title = title;
            Description = description;
            UpVote = upVote;
            DownVote = downVote;
            PublicationDate = date;
            this.Comments = comments;
        }
    }
}
