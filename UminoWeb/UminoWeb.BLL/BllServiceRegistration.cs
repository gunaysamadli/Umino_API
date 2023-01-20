using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.BLL.Dto;
using UminoWeb.BLL.Services;
using UminoWeb.BLL.Services.contracts;
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

            return services;
        }
    }
}
