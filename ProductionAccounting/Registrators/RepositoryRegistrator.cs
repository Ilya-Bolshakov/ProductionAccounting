using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.DAL;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.DAL.EntitiesRepositories;
using ProductionAccounting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Registrators
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddTransient<IRepository<Employee>, DbRepository<Employee>>()
            .AddTransient<IRepository<Operation>, OperationRepository>()
            .AddTransient<IRepository<OperationСoefficient>, DbRepository<OperationСoefficient>>()
            .AddTransient<IRepository<Product>, DbRepository<Product>>();
    }
}
