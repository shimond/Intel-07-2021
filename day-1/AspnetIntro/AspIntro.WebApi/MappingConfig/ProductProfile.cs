using AspIntro.WebApi.Models;
using AspIntro.WebApi.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIntro.WebApi.MappingConfig
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ExpireDate, opt => opt.MapFrom(src => src.ExpireDate))
                .ReverseMap();
        }
    }
}
