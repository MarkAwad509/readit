using System.ComponentModel.DataAnnotations;

namespace Readit.Models
{
    public class User
    {
        private static int AUTO_ID = -1;
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; }

        public User(int id, string username, string email, string password) {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
        }
        
        public User(string username, string email, string password) {
            Id = AUTO_ID++;
            Username = username;
            Email = email;
            Password = password;
        }

        public override string? ToString() {
            return $"username={this.Username}, email={this.Email}, password={this.Password}";
        }
    }
}
