

namespace FashionShop.Contracts.Catalog
{
    public class CreateProductDto
    {
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public decimal BasePrice { get; set; }
    }
}
