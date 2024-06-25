using System;
using System.Collections.Generic;

namespace BusinnessObjects
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            ExchangeRequests = new HashSet<ExchangeRequest>();
            Ratings = new HashSet<Rating>();
            Reports = new HashSet<Report>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int? SellerId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Category? Category { get; set; }
        public virtual User? Seller { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ExchangeRequest> ExchangeRequests { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
