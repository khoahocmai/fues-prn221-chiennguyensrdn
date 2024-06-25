using System;
using System.Collections.Generic;

namespace BusinnessObjects
{
    public partial class User
    {
        public User()
        {
            BanAdmins = new HashSet<Ban>();
            BanUsers = new HashSet<Ban>();
            Comments = new HashSet<Comment>();
            ExchangeRequests = new HashSet<ExchangeRequest>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
            Products = new HashSet<Product>();
            Ratings = new HashSet<Rating>();
            Reports = new HashSet<Report>();
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;

        public virtual ICollection<Ban> BanAdmins { get; set; }
        public virtual ICollection<Ban> BanUsers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ExchangeRequest> ExchangeRequests { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
