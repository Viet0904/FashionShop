using FashionShop.Domain.Users;
using FashionShop.Domain.Catalog;
using FashionShop.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Infrastructure.Persistence
{

    public class FashionShopDbContext : DbContext
    {
        public FashionShopDbContext(DbContextOptions<FashionShopDbContext> options)
            : base(options)
        {
        }

        // === DbSet cho tất cả entity trong Domain ===

        // Users
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserPermission> UserPermissions => Set<UserPermission>();

        // Catalog
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Size> Sizes => Set<Size>();
        public DbSet<Color> Colors => Set<Color>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<Collection> Collections => Set<Collection>();
        public DbSet<ProductCollection> ProductCollections => Set<ProductCollection>();

        // Orders / Cart / Vouchers
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Voucher> Vouchers => Set<Voucher>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUsers(modelBuilder);
            ConfigureMenus(modelBuilder);
            ConfigureCatalog(modelBuilder);
            ConfigureOrders(modelBuilder);
        }

        // ================== CONFIG USERS + ROLES ==================

        private static void ConfigureUsers(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(x => x.Id);

                e.Property(x => x.Email)
                    .HasMaxLength(255)
                    .IsRequired();

                e.HasIndex(x => x.Email)
                    .IsUnique();

                e.Property(x => x.Password)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(x => x.UserName)
                    .HasMaxLength(100)
                    .IsRequired();

                e.HasIndex(x => x.UserName)
                    .IsUnique();

                e.Property(x => x.FullName)
                    .HasMaxLength(255);

                e.Property(x => x.Phone)
                    .HasMaxLength(20);

                e.Property(x => x.AvatarUrl)
                    .HasMaxLength(500);

                e.Property(x => x.Status)
                    .HasConversion<byte>(); // map enum -> tinyint
            });

            // Role
            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Roles");
                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                e.HasIndex(x => x.Name)
                    .IsUnique();

                e.Property(x => x.DisplayName)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(x => x.Description)
                    .HasMaxLength(500);
            });

            // UserRole (N-N)
            modelBuilder.Entity<UserRole>(e =>
            {
                e.ToTable("UserRoles");
                e.HasKey(x => new { x.UserId, x.RoleId });

                e.HasOne(x => x.User)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.UserId);

                e.HasOne(x => x.Role)
                    .WithMany(x => x.UserRoles)
                    .HasForeignKey(x => x.RoleId);
            });
        }

        // ================== CONFIG MENU + PERMISSION ==================

        private static void ConfigureMenus(ModelBuilder modelBuilder)
        {
            // Menu
            modelBuilder.Entity<Menu>(e =>
            {
                e.ToTable("Menus");
                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(x => x.Icon)
                    .HasMaxLength(100);

                e.Property(x => x.Path)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(x => x.SortOrder)
                    .IsRequired();

                // Self reference
                e.HasOne(x => x.Parent)
                    .WithMany(x => x.Children)
                    .HasForeignKey(x => x.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // RolePermission
            modelBuilder.Entity<RolePermission>(e =>
            {
                e.ToTable("RolePermissions");

                // Nếu muốn dùng composite key (RoleId, MenuId):
                e.HasKey(x => new { x.RoleId, x.MenuId });

                e.HasOne(x => x.Role)
                    .WithMany(x => x.RolePermissions)
                    .HasForeignKey(x => x.RoleId);

                e.HasOne(x => x.Menu)
                    .WithMany(x => x.RolePermissions)
                    .HasForeignKey(x => x.MenuId);
            });

            // UserPermission
            modelBuilder.Entity<UserPermission>(e =>
            {
                e.ToTable("UserPermissions");

                e.HasKey(x => new { x.UserId, x.MenuId });

                e.HasOne(x => x.User)
                    .WithMany(x => x.UserPermissions)
                    .HasForeignKey(x => x.UserId);

                e.HasOne(x => x.Menu)
                    .WithMany(x => x.UserPermissions)
                    .HasForeignKey(x => x.MenuId);
            });
        }

        // ================== CONFIG CATALOG ==================

        private static void ConfigureCatalog(ModelBuilder modelBuilder)
        {
            // Category
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Categories");
                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(x => x.Slug)
                    .HasMaxLength(255)
                    .IsRequired();

                e.HasIndex(x => x.Slug)
                    .IsUnique();

                e.Property(x => x.Description)
                    .HasMaxLength(1000);

                e.Property(x => x.IsActive)
                    .IsRequired();

                e.HasOne(x => x.Parent)
                    .WithMany(x => x.Children)
                    .HasForeignKey(x => x.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Brand
            modelBuilder.Entity<Brand>(e =>
            {
                e.ToTable("Brands");
                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(x => x.Slug)
                    .HasMaxLength(255)
                    .IsRequired();

                e.HasIndex(x => x.Slug)
                    .IsUnique();
            });

            // Size
            modelBuilder.Entity<Size>(e =>
            {
                e.ToTable("Sizes");
                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            // Color
            modelBuilder.Entity<Color>(e =>
            {
                e.ToTable("Colors");
                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                e.Property(x => x.HexCode)
                    .HasMaxLength(7) // #RRGGBB
                    .IsRequired();
            });

            // Product
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(x => x.Slug)
                    .HasMaxLength(255)
                    .IsRequired();

                e.HasIndex(x => x.Slug)
                    .IsUnique();

                e.Property(x => x.ThumbnailUrl)
                    .HasMaxLength(500);

                e.Property(x => x.BasePrice)
                    .HasColumnType("decimal(18,2)");

                e.Property(x => x.Status)
                    .HasConversion<byte>();

                e.HasOne(x => x.Category)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Brand)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ProductImage
            modelBuilder.Entity<ProductImage>(e =>
            {
                e.ToTable("ProductImages");
                e.HasKey(x => x.Id);

                e.Property(x => x.ImageUrl)
                    .HasMaxLength(500)
                    .IsRequired();

                e.Property(x => x.SortOrder)
                    .IsRequired();

                e.HasOne(x => x.Product)
                    .WithMany(x => x.Images)
                    .HasForeignKey(x => x.ProductId);
            });

            // ProductVariant
            modelBuilder.Entity<ProductVariant>(e =>
            {
                e.ToTable("ProductVariants");
                e.HasKey(x => x.Id);

                e.Property(x => x.Price)
                    .HasColumnType("decimal(18,2)");

                e.Property(x => x.Stock)
                    .IsRequired();

                e.HasOne(x => x.Product)
                    .WithMany(x => x.Variants)
                    .HasForeignKey(x => x.ProductId);

                e.HasOne(x => x.Size)
                    .WithMany(x => x.Variants)
                    .HasForeignKey(x => x.SizeId);

                e.HasOne(x => x.Color)
                    .WithMany(x => x.Variants)
                    .HasForeignKey(x => x.ColorId);
            });

            // Collection
            modelBuilder.Entity<Collection>(e =>
            {
                e.ToTable("Collections");
                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(x => x.Slug)
                    .HasMaxLength(255)
                    .IsRequired();

                e.HasIndex(x => x.Slug)
                    .IsUnique();

                e.Property(x => x.BannerUrl)
                    .HasMaxLength(500);
            });

            // ProductCollection (N-N)
            modelBuilder.Entity<ProductCollection>(e =>
            {
                e.ToTable("ProductCollections");
                e.HasKey(x => new { x.ProductId, x.CollectionId });

                e.HasOne(x => x.Product)
                    .WithMany(x => x.ProductCollections)
                    .HasForeignKey(x => x.ProductId);

                e.HasOne(x => x.Collection)
                    .WithMany(x => x.ProductCollections)
                    .HasForeignKey(x => x.CollectionId);
            });
        }

        // ================== CONFIG ORDERS / CART / VOUCHERS ==================

        private static void ConfigureOrders(ModelBuilder modelBuilder)
        {
            // Cart
            modelBuilder.Entity<Cart>(e =>
            {
                e.ToTable("Carts");
                e.HasKey(x => x.Id);

                e.HasOne(x => x.User)
                    .WithMany() // tạm thời không cần navigation ngược
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // CartItem
            modelBuilder.Entity<CartItem>(e =>
            {
                e.ToTable("CartItems");

                // dùng composite key (CartId, VariantId)
                e.HasKey(x => new { x.CartId, x.VariantId });

                e.Property(x => x.Quantity)
                    .IsRequired();

                e.HasOne(x => x.Cart)
                    .WithMany(x => x.Items)
                    .HasForeignKey(x => x.CartId);

                e.HasOne(x => x.Variant)
                    .WithMany()
                    .HasForeignKey(x => x.VariantId);
            });

            // Order
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Orders");
                e.HasKey(x => x.Id);

                e.Property(x => x.TotalAmount)
                    .HasColumnType("decimal(18,2)");

                e.Property(x => x.DiscountAmount)
                    .HasColumnType("decimal(18,2)");

                e.Property(x => x.FinalAmount)
                    .HasColumnType("decimal(18,2)");

                e.Property(x => x.ReceiverName)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(x => x.ReceiverPhone)
                    .HasMaxLength(20)
                    .IsRequired();

                e.Property(x => x.ShippingAddress)
                    .HasMaxLength(500)
                    .IsRequired();

                e.Property(x => x.Status)
                    .HasConversion<byte>();

                e.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Voucher)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(x => x.VoucherId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // OrderItem
            modelBuilder.Entity<OrderItem>(e =>
            {
                e.ToTable("OrderItems");

                e.HasKey(x => new { x.OrderId, x.VariantId });

                e.Property(x => x.UnitPrice)
                    .HasColumnType("decimal(18,2)");

                e.Property(x => x.Quantity)
                    .IsRequired();

                e.HasOne(x => x.Order)
                    .WithMany(x => x.Items)
                    .HasForeignKey(x => x.OrderId);

                e.HasOne(x => x.Variant)
                    .WithMany()
                    .HasForeignKey(x => x.VariantId);
            });

            // Voucher
            modelBuilder.Entity<Voucher>(e =>
            {
                e.ToTable("Vouchers");
                e.HasKey(x => x.Id);

                e.Property(x => x.Code)
                    .HasMaxLength(50)
                    .IsRequired();

                e.HasIndex(x => x.Code)
                    .IsUnique();

                e.Property(x => x.Type)
                    .HasConversion<byte>();

                e.Property(x => x.Value)
                    .HasColumnType("decimal(18,2)");

                e.Property(x => x.MaxDiscount)
                    .HasColumnType("decimal(18,2)");

                e.Property(x => x.IsActive)
                    .IsRequired();
            });
        }
    }
}
