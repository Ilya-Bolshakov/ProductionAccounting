﻿using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services;
using ProductionAccounting.Services.Interfaces;

namespace ProductionAccounting.Registrators
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IUserDialog<EmployeeModel>, EmployeeUserDialogService>()
            .AddTransient<IUserDialog<CoefficientModel>, CoefficientUserDialogService>()
            .AddTransient<IUserDialogWithRepository<OperationModel, OperationСoefficient>, OperationUserDialogService>()
            .AddTransient<IUserDialogWithRepository<ProductModel, Operation>, ProductUserDialogService>()
            ;
    }
}
