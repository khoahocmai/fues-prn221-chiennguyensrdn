using System;
using System.Collections.Generic;

namespace BusinnessObjects
{
    public partial class Categorie
    {
        public Categorie()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
