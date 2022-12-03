using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Readit.Models.Entities
{
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }
        public int? MemberId { get; set; }
        public int? LinkId { get; set; }
        public string? Content { get; set; }
        public DateTime? PublicationDate { get; set; }
        public virtual Link? Link { get; set; }
        public virtual Member? Member { get; set; }
    }
}
