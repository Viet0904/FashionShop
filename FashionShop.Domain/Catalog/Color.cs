using FashionShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Catalog
{
    public class Color : AuditableEntity<int>
    {
        public string Name { get; set; } = null!;
        public string HexCode { get; set; } = null!;

        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}
