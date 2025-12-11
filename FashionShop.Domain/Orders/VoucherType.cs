using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Orders
{
    public enum VoucherType : byte
    {
        Percent = 0,
        FixedAmount = 1
    }
}
