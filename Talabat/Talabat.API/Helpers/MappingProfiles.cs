using AutoMapper;
using Talabat.API.DTOs;
using Talabat.Core.Entities;

namespace Talabat.API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                     .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                     .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                     .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<CustomerBasket, CustomerBasketDto >();



        }
    }
}
