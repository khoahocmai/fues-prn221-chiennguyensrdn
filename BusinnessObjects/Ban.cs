using System;
using System.Collections.Generic;

namespace BusinnessObjects
{
    public partial class Ban
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? AdminId { get; set; }
        public string? Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual User? Admin { get; set; }
        public virtual User? User { get; set; }
    }
}
