using Business.Interfaces;
using Business.Services;
using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using MiningVentilation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiningVentilation.Extensions
{
    public static class RegisterServices
    {
        public static void RegisterBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IUsageService, UsageService>();

            services.AddScoped<IDataContext, DataContext>();
            services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

        }
    }
}
