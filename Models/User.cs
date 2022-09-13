namespace projetwebserver.Models
{
    public class User
    {
        public string UserName { get; set; }  
        public string Password { get; set; }    

        public string Email { get; set; }   
        public string Name { get; set; }    
        public string phone { get; set; }   

        public User(string userName, string password, string email, string name, string phone)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Name = name;
            this.phone = phone;
        }
    }
}
