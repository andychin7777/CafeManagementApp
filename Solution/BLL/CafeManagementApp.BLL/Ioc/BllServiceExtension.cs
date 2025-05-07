using CafeManagementApp.BLL.Interface;
using CafeManagementApp.BLL.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CafeManagementApp.BLL.Ioc
{
    public static class BllServiceExtension
    {
        public static IServiceCollection AddBll(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICafeService, CafeService>();
            return services;
        }
    }
}
