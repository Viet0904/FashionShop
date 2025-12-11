using FashionShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Catalog
{
    public class Size : AuditableEntity<int>
    {
        public string Name { get; set; } = null!;
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}
