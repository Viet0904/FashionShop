

namespace FashionShop.Contracts.Common
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? ThumbnailUrl { get; set; }
        public decimal BasePrice { get; set; }

        // aggregate from variants
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int ViewsCount { get; set; }
        public int SoldCount { get; set; }
    }
}
