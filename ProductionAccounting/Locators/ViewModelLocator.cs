using Microsoft.Extensions.DependencyInjection;
using ProductionAccounting.ViewModels;

namespace ProductionAccounting.Locators
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
