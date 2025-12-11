using FashionShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionShop.Domain.Common;
namespace FashionShop.Domain.Users
{
    public class User : AuditableEntity<int>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public UserStatus Status { get; set; }

        public DateTime? EmailVerifiedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
}
