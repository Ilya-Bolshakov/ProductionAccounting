using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.ViewModels;

namespace ProductionAccounting.Locators
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public EmployeeViewModel EmployeeViewModel => App.Services.GetRequiredService<EmployeeViewModel>();
        public ProductsViewModel OperationViewModel => App.Services.GetRequiredService<ProductsViewModel>();
        public EmployeeEditorWindowViewModel EmployeeEditorWindowViewModel => App.Services.GetRequiredService<EmployeeEditorWindowViewModel>();
    }
}
