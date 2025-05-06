using CafeManagementApp.DAL.Interface;
using CafeManagementApp.DAL.Service;
using CafeManagementApp.DAL.Service.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace CafeManagementApp.DAL.Ioc
{
    public static class DalServiceExtension
    {
        public static IServiceCollection AddDal(this IServiceCollection services)
        {
            services.AddScoped<ICafeEmployeeRepository, CafeEmployeeRepository>();
            services.AddScoped<ICafeRepository, CafeRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
