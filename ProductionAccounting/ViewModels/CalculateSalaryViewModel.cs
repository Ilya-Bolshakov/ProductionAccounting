using MathCore.WPF.ViewModels;
using ProductionAccounting.Services.Interfaces;

namespace ProductionAccounting.ViewModels
{
    public class CalculateSalaryViewModel : ViewModel
    {
        private readonly ICalculateSalaryService _service;
        public CalculateSalaryViewModel(ICalculateSalaryService service)
        {
            _service = service;
        }
    }
}
