using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.DAL.Context;

namespace ProductionAccounting.Registrators
{
    public static class DbRegistrator
    {
        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<ProductionAccountingContext>(opt =>
            {
                //opt.UseSqlServer(configuration.GetConnectionString("DevDB"))
                opt.UseMySql(configuration.GetConnectionString("MySql"), ServerVersion.AutoDetect(configuration.GetConnectionString("MySql")))
                ;
                //opt.UseSqlServer(configuration["MSSQL"]);
            });
    }
}
