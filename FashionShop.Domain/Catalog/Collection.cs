using FashionShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Catalog
{
    public class Collection : AuditableEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? BannerUrl { get; set; }

        public ICollection<ProductCollection> ProductCollections { get; set; } = new List<ProductCollection>();
    }
}
