using System;
using System.Collections.Generic;

namespace BusinnessObjects
{
    public partial class Report
    {
        public int Id { get; set; }
        public int? ReporterId { get; set; }
        public int? ProductId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? Reporter { get; set; }
    }
}
