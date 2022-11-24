using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Readit.Models.Entities
{
    public partial class Link
    {
        [Key]
        public int Id { get; set; }
        public int? MemberId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? UpVote { get; set; }
        public int? DownVote { get; set; }
        public DateTime? PublicationDate { get; set; }
        public virtual Member? Member { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        public Link()
        {
            Comments = new HashSet<Comment>();
            Votes = new HashSet<Vote>();
        }
    }
}
