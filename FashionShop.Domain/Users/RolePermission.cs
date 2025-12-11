using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Users
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }

        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }

        public Role Role { get; set; } = null!;
        public Menu Menu { get; set; } = null!;
    }
}
