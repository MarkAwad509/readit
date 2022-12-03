using System;
using System.Collections.Generic;

namespace Readit.Models.Entities
{
    public partial class Vote
    {
        public int Id { get; set; }
        public int? MemberId { get; set; }
        public int? LinkId { get; set; }
        public bool IsUpVote { get; set; }

        public virtual Link Link { get; set; }
        public virtual Member Member { get; set; }
    }
}
