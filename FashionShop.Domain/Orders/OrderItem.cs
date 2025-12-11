using FashionShop.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Orders
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int VariantId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Order Order { get; set; } = null!;
        public ProductVariant Variant { get; set; } = null!;
    }
}
