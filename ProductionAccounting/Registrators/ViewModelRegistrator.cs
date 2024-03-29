﻿using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.ViewModels;

namespace ProductionAccounting.Registrators
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddScoped<MainWindowViewModel>()
            .AddTransient<EmployeeViewModel>()
            .AddTransient<ProductsViewModel>()
            .AddTransient<CoefficientViewModel>()
            .AddTransient<OperationsViewModel>()
            .AddTransient<WorkDataViewModel>()
            .AddTransient<InsertDataViewModel>()
            .AddTransient<CalculateSalaryViewModel>()
        ;
    }
}
