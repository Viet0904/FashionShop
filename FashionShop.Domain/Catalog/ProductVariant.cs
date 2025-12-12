using FashionShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Catalog
{
    public class ProductVariant : AuditableEntity<int>
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }

        public decimal Price { get; set; }
        
        public int StockQuantity { get; set; }

        public Product Product { get; set; } = null!;
        public Size Size { get; set; } = null!;
        public Color Color { get; set; } = null!;
    }
}
