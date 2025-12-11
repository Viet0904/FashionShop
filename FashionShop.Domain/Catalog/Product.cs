using FashionShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Catalog
{
    public class Product : AuditableEntity<int>
    {
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }

        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }

        public decimal BasePrice { get; set; }
        public ProductStatus Status { get; set; }

        public int ViewsCount { get; set; }
        public int SoldCount { get; set; }

        public Category Category { get; set; } = null!;
        public Brand? Brand { get; set; }

        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductCollection> ProductCollections { get; set; } = new List<ProductCollection>();
    }
}
