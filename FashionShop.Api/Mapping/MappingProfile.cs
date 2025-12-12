using AutoMapper;
using FashionShop.Contracts.Catalog;
using FashionShop.Domain.Catalog;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace FashionShop.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.Variants != null && src.Variants.Any() ? (decimal?)src.Variants.Min(v => v.Price) : null))
                .ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src => src.Variants != null && src.Variants.Any() ? (decimal?)src.Variants.Max(v => v.Price) : null));
        }
    }
}
