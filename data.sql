-- =============================================
-- 1. ROLES
-- =============================================
INSERT INTO [Roles] ([Name], [DisplayName], [Description])
VALUES 
('admin', N'Quản trị viên', N'Quyền cao nhất'),
('customer', N'Khách hàng', N'Người dùng thông thường');

-- =============================================
-- 2. USERS (Admin)
-- =============================================
INSERT INTO [Users] (
    [Email], [Password], [UserName], [FullName], [Phone], [AvatarUrl], 
    [Status], [EmailVerifiedAt], [LastLoginAt]
)
VALUES (
    'admin@shop.com',
    '$2a$12$wVU0cOmrUOqjc7YRh.ZATOtaqu42oZP6BWjDQ1RAseeuYVV.B56Fy',
    'admin',
    N'Administrator',
    '0901234567',
    NULL,
    1,  -- Active
    '2025-01-01 00:00:00',
    '2025-12-15 00:00:00'
);

-- Gán role admin cho user admin (UserId = 1)
INSERT INTO [UserRoles] ([UserId], [RoleId])
VALUES (1, 1);  -- 1 là Id của role admin

-- =============================================
-- 3. SIZES
-- =============================================
INSERT INTO [Sizes] ([Name], [Description])
VALUES 
('S', N'Small'),
('M', N'Medium'),
('L', N'Large'),
('XL', N'Extra Large'),
('XXL', N'Extra Extra Large');

-- =============================================
-- 4. COLORS
-- =============================================
INSERT INTO [Colors] ([Name], [HexCode])
VALUES 
(N'Trắng', '#FFFFFF'),
(N'Đen', '#000000'),
(N'Xanh dương', '#0000FF'),
(N'Đỏ', '#FF0000'),
(N'Xám', '#808080');

-- =============================================
-- 5. BRANDS
-- =============================================
INSERT INTO [Brands] ([Name], [Slug], [LogoUrl], [Description], [IsActive])
VALUES 
('Nike', 'nike', 'https://example.com/logos/nike.png', N'Thương hiệu thể thao nổi tiếng', 1),
('Adidas', 'adidas', 'https://example.com/logos/adidas.png', N'Thương hiệu thể thao Đức', 1);

-- =============================================
-- 6. CATEGORIES
-- =============================================
INSERT INTO [Categories] ([ParentId], [Name], [Slug], [Description], [SortOrder], [IsActive])
VALUES 
(NULL, N'Áo', 'ao', N'Các loại áo', 1, 1),
(NULL, N'Quần', 'quan', N'Các loại quần', 2, 1),
(NULL, N'Phụ kiện', 'phu-kien', N'Mũ, túi, ví...', 3, 1),

(1, N'Áo thun', 'ao-thun', N'Áo thun cotton', 1, 1),
(1, N'Áo hoodie', 'ao-hoodie', N'Áo có mũ', 2, 1),
(2, N'Quần jeans', 'quan-jeans', N'Quần bò', 1, 1);

-- =============================================
-- 7. PRODUCTS
-- =============================================
INSERT INTO [Products] (
    [CategoryId], [BrandId], [Name], [Slug], [Description], [ThumbnailUrl], 
    [BasePrice], [Status], [ViewsCount], [SoldCount]
)
VALUES 
(4, 1, N'Áo thun Nike Basic', 'ao-thun-nike-basic', N'Áo thun cotton thoải mái', 'https://example.com/thumbs/nike-tshirt.jpg', 350000, 1, 150, 50),
(6, 2, N'Quần jeans Adidas Slim', 'quan-jeans-adidas-slim', N'Quần jeans ôm gọn', 'https://example.com/thumbs/adidas-jeans.jpg', 1200000, 1, 200, 30),
(3, NULL, N'Mũ lưỡi trai Classic', 'mu-long-trai-classic', N'Mũ thời trang', 'https://example.com/thumbs/cap.jpg', 250000, 1, 80, 20);

-- =============================================
-- 8. PRODUCT CATEGORIES (N-N, thêm category phụ)
-- =============================================
-- Áo thun Nike thuộc cả Áo và Áo thun (đã có CategoryId chính là 4)
INSERT INTO [ProductCategories] ([ProductId], [CategoryId])
VALUES 
(1, 1),  -- thuộc Áo cha
(2, 2);  -- Quần jeans thuộc Quần cha

-- =============================================
-- 9. PRODUCT IMAGES
-- =============================================
INSERT INTO [ProductImages] ([ProductId], [ImageUrl], [IsMain], [SortOrder])
VALUES 
(1, 'https://example.com/images/nike-tshirt-1.jpg', 1, 1),
(1, 'https://example.com/images/nike-tshirt-2.jpg', 0, 2),
(2, 'https://example.com/images/adidas-jeans-1.jpg', 1, 1),
(3, 'https://example.com/images/cap-1.jpg', 1, 1);

-- =============================================
-- 10. PRODUCT VARIANTS
-- =============================================
INSERT INTO [ProductVariants] (
    [ProductId], [SizeId], [ColorId], [Sku], [VariantName], 
    [Price], [StockQuantity], [IsActive]
)
VALUES 
(1, 1, 1, 'NIKE-TS-S-W', N'Áo thun Nike Size S Trắng', 350000, 20, 1),
(1, 2, 2, 'NIKE-TS-M-B', N'Áo thun Nike Size M Đen', 350000, 15, 1),
(1, 3, 1, 'NIKE-TS-L-W', N'Áo thun Nike Size L Trắng', 350000, 10, 1),

(2, 3, 3, 'ADI-JEANS-L-BLUE', N'Quần jeans Adidas Size L Xanh dương', 1200000, 8, 1),
(2, 4, 2, 'ADI-JEANS-XL-BLACK', N'Quần jeans Adidas Size XL Đen', 1200000, 5, 1),

(3, NULL, 2, 'CAP-BLACK', N'Mũ lưỡi trai Đen', 250000, 30, 1);

-- =============================================
-- 11. COLLECTIONS
-- =============================================
INSERT INTO [Collections] ([Name], [Slug], [Description], [IsActive])
VALUES (N'Summer Collection 2025', 'summer-2025', N'Bộ sưu tập hè năng động', 1);

-- Gán products vào collection
INSERT INTO [ProductCollections] ([ProductId], [CollectionId])
VALUES 
(1, 1),
(2, 1),
(3, 1);

-- =============================================
-- HOÀN TẤT
-- =============================================