using FashionShop.Domain.Common;
using FashionShop.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Orders
{
    public class Cart : AuditableEntity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
