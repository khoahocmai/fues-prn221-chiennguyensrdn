using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class ExchangeRequest
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? RequesterId { get; set; }
        public string? Message { get; set; }
        public string Status { get; set; } = null!; // 'Pending', 'Accepted', 'Rejected'
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? Requester { get; set; }
    }
}
