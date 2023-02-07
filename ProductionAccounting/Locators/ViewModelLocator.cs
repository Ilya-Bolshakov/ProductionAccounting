using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.ViewModels;

namespace ProductionAccounting.Locators
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public EmployeeViewModel EmployeeViewModel => App.Services.GetRequiredService<EmployeeViewModel>();
        public OperationViewModel OperationViewModel => App.Services.GetRequiredService<OperationViewModel>();
        public EmployeeEditorWindowViewModel EmployeeEditorWindowViewModel => App.Services.GetRequiredService<EmployeeEditorWindowViewModel>();
    }
}
