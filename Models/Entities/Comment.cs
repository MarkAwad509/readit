namespace Readit.Models.Entities
{
    public class Comment
    {
        public int ID { get; set; }
        public Member User { get; set; }
        public string Content { get; set; }
        public DateTime Publication { get; set; }

        public Comment() { }

        public Comment(int iD, Member user, string content, DateTime publication)
        {
            this.ID = iD;
            this.User = user;
            this.Content = content;
            this.Publication = publication;
        }
    }
}
