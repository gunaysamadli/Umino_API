using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UminoWeb.BLL.Dto;
using UminoWeb.BLL.Services;
using UminoWeb.BLL.Services.contracts;
using UminoWeb.BLL.Validators.CategoryValidator;
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

            return services;
        }
    }
}
