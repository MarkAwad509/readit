﻿using System;
using System.Collections.Generic;

namespace Readit.Models.Entities
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? MemberId { get; set; }
        public int? LinkId { get; set; }
        public string? Content { get; set; }
        public DateTime? PublicationDate { get; set; }

        public virtual Link? Link { get; set; }
        public virtual Member? Member { get; set; }
    }
}
