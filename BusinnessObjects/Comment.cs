using System;
using System.Collections.Generic;

namespace BusinnessObjects
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public string? Comment1 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
