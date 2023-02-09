using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.DAL;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.DAL.EntitiesRepositories;
using ProductionAccounting.Interfaces;

namespace ProductionAccounting.Registrators
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddTransient<IRepository<Employee>, DbRepository<Employee>>()
            .AddTransient<IRepository<Operation>, OperationRepository>()
            .AddTransient<IRepository<OperationСoefficient>, DbRepository<OperationСoefficient>>()
            .AddTransient<IRepository<Product>, ProductRepository>()
            .AddTransient<IRepository<ExecutedOperation>, ExecutedOperationRepository>();
    }
}
