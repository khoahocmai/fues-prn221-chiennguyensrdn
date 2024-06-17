﻿using System;
using System.Collections.Generic;

namespace BusinnessObjects
{
    public partial class ExchangeRequest
    {
        public int Id { get; set; }
        public int? OwnProductId { get; set; }
        public int? ProductId { get; set; }
        public int? RequesterId { get; set; }
        public string? Message { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Product? OwnProduct { get; set; }
        public virtual Product? Product { get; set; }
        public virtual User? Requester { get; set; }
    }
}