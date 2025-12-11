using FashionShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Domain.Users
{
    public class Menu : AuditableEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public string Path { get; set; } = null!;
        public int SortOrder { get; set; }

        public int? ParentId { get; set; }
        public Menu? Parent { get; set; }
        public ICollection<Menu> Children { get; set; } = new List<Menu>();

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
}
