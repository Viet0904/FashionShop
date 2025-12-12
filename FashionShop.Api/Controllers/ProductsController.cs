using AutoMapper;
using FashionShop.Application.Catalog;
using FashionShop.Contracts.Catalog;
using FashionShop.Domain.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(CancellationToken ct)
        {
            var products = await _productService.GetAllAsync(ct);
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetById(int id, CancellationToken ct)
        {
            var product = await _productService.GetByIdAsync(id, ct);
            if (product == null) return NotFound();

            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                ThumbnailUrl = product.ThumbnailUrl,
                BasePrice = product.BasePrice,
                MinPrice = product.Variants?.Any() == true ? product.Variants.Min(v => (decimal?)v.Price) : null,
                MaxPrice = product.Variants?.Any() == true ? product.Variants.Max(v => (decimal?)v.Price) : null,
                ViewsCount = product.ViewsCount,
                SoldCount = product.SoldCount
            };

            return Ok(dto);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto, CancellationToken ct)
        {
            var p = new Product
            {
                CategoryId = dto.CategoryId,
                BrandId = dto.BrandId,
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                ThumbnailUrl = dto.ThumbnailUrl,
                BasePrice = dto.BasePrice,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = ProductStatus.Active
            };

            var created = await _productService.CreateAsync(p, ct);
            var mapped = _mapper.Map<ProductDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = mapped.Id }, mapped);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProductDto dto, CancellationToken ct)
        {
            var existing = await _productService.GetByIdAsync(id, ct);
            if (existing == null) return NotFound();

            existing.CategoryId = dto.CategoryId;
            existing.BrandId = dto.BrandId;
            existing.Name = dto.Name;
            existing.Slug = dto.Slug;
            existing.Description = dto.Description;
            existing.ThumbnailUrl = dto.ThumbnailUrl;
            existing.BasePrice = dto.BasePrice;
            existing.UpdatedAt = DateTime.UtcNow;

            await _productService.UpdateAsync(existing, ct);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _productService.DeleteAsync(id, ct);
            return NoContent();
        }


    }
}
