﻿using System;
using System.Collections.Generic;

namespace Readit.Models.Entities
{
    public partial class Member
    {
        public Member()
        {
            Comments = new HashSet<Comment>();
            Links = new HashSet<Link>();
            Votes = new HashSet<Vote>();
        }
        public Member(string Username,string Email,string Password)
        {
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Link> Links { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
