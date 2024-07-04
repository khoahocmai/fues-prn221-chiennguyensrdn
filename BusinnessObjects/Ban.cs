using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class Ban
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? ModeratorId { get; set; }
        public string? Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual User? Moderator { get; set; }
        public virtual Product? Product { get; set; }
    }
}
