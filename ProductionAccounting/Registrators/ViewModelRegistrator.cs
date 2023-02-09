using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Registrators
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddTransient<EmployeeViewModel>()
            .AddTransient<OperationViewModel>()
            .AddTransient<EmployeeEditorWindowViewModel>()
        ;
    }
}
