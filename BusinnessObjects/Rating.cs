using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public int? Rating1 { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
