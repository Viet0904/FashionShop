using FashionShop.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Orders
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int VariantId { get; set; }

        public int Quantity { get; set; }

        public Cart Cart { get; set; } = null!;
        public ProductVariant Variant { get; set; } = null!;
    }
}
