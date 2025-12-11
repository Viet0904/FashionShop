using FashionShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Orders
{
    public class Voucher : AuditableEntity<int>
    {
        public string Code { get; set; } = null!;
        public VoucherType Type { get; set; }
        public decimal Value { get; set; }
        public decimal? MaxDiscount { get; set; }
        public int Quantity { get; set; }

        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
