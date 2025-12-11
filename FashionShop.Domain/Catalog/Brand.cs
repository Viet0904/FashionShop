using FashionShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Catalog
{
    public class Brand : AuditableEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
