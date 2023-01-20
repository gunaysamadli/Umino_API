using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.DAL.Repositories.Contracts;
using UminoWeb.DAL.Repositories;

namespace UminoWeb.DAL
{
    public static class DataAccessLayerServiceRegistration
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

            return services;
        }
    }
}
