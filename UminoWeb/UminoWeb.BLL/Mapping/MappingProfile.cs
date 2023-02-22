using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.BLL.Dto;
using UminoWeb.DAL.Entities;

namespace UminoWeb.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Slider, SliderDto>().ReverseMap();
            CreateMap<Slider, SliderCreateDto>().ReverseMap();
            CreateMap<Slider, SliderUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();

            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, BrandCreateDto>().ReverseMap();
            CreateMap<Brand, BrandUpdateDto>().ReverseMap();

            CreateMap<Color, ColorDto>().ReverseMap();
            CreateMap<Color, ColorCreateDto>().ReverseMap();
            CreateMap<Color, ColorUpdateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();

            CreateMap<ProductColor, ProductColorDto>().ReverseMap();
            CreateMap<ProductColor, ProductColorCreateDto>().ReverseMap();
            CreateMap<ProductColor, ProductColorUpdateDto>().ReverseMap();
            CreateMap<ProductColor, GeneralProductColorDto>().ReverseMap();

            CreateMap<Product, GeneralProductDto>().ReverseMap();

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();
            CreateMap<ProductImage, GeneralProductImageDto>().ReverseMap();
            CreateMap<ProductImage, ProductImageCreateDto>().ReverseMap();

            CreateMap<ApplicationUser, UserDto>().ReverseMap();

        }
    }
}
