using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Readit.Models.Entities
{
    public partial class Member
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Link> Links { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
