namespace Readit.Models.Entities
{
    public class Member
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Member() { }

        public Member(string username, string email, string password)
        {
            this.ID = 0;
            this.Username = username;
            this.Email = email;
            this.Password = password;
        }

        public Member(int id, string username, string email, string password)
        {
            this.ID = id;
            this.Username = username;
            this.Email = email;
            this.Password = password;
        }
    }
}
