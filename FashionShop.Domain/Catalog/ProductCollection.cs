using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Catalog
{
    public class ProductCollection
    {
        public int ProductId { get; set; }
        public int CollectionId { get; set; }

        public Product Product { get; set; } = null!;
        public Collection Collection { get; set; } = null!;
    }
}
