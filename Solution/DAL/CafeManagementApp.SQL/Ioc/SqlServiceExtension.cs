using CafeManagementApp.SQL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CafeManagementApp.SQL.Ioc
{
    public static class SqlServiceExtension
    {
        public static IServiceCollection AddSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CafeManagementDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CafeManagementAppDBContext"));
            });
            return services;
        }
    }
}
