using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Orders
{
    public enum OrderStatus : byte
    {
        Pending = 0,
        Confirmed = 1,
        Shipping = 2,
        Completed = 3,
        Cancelled = 4
    }
}
