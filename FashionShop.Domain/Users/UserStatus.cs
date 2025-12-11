using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Users
{
    public enum UserStatus : byte
    {
        Inactive = 0,
        Active = 1,
        Banned = 2
    }

}
