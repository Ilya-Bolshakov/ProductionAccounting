﻿using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.Services;
using ProductionAccounting.Services.Interfaces;

namespace ProductionAccounting.Registrators
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IUserDialog, UserDialogService>()
            ;
    }
}