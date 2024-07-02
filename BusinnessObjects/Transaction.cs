using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int? BuyerId { get; set; }
        public int? ProductId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User? Buyer { get; set; }
        public virtual Product? Product { get; set; }
    }
}
