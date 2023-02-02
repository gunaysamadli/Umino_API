using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UminoWeb.BLL.Dto;
using UminoWeb.BLL.Services;
using UminoWeb.BLL.Services.contracts;
using UminoWeb.BLL.Validators.BrandValidator;
using UminoWeb.BLL.Validators.CategoryValidator;
using UminoWeb.BLL.Validators.ColorValidator;
using UminoWeb.BLL.Validators.ProductImageValidator;
using UminoWeb.BLL.Validators.ProductValidator;
using UminoWeb.BLL.Validators.SliderValidator;

namespace UminoWeb.BLL
{
    public static class BllServiceRegistration
    {
        public static IServiceCollection AddBllServices(this IServiceCollection services)
        {

            services.AddScoped<ISliderService, SliderManager>();
            services.AddScoped<IValidator<SliderCreateDto>, SliderCreateDtoValidation>();
            services.AddScoped<IValidator<SliderUpdateDto>, SliderUpdateDtoValidation>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IValidator<CategoryCreateDto>, CategoryCreateDtoValidation>();

            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<IValidator<BrandCreateDto>, BrandCreateDtoValidation>();

            services.AddScoped<IColorService, ColorManager>();
            services.AddScoped<IValidator<ColorCreateDto>, ColorCreateDtoValidation>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateDtoValidation>();

            services.AddScoped<IProductColorService, ProductColorManager>();

            services.AddScoped<IProductImageService, ProductImageManager>();
            services.AddScoped<IValidator<ProductImageCreateDto>, ProductImageCreateDtoValidation>();

            return services;
        }
    }
}
