using MathCore.WPF.ViewModels;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;

namespace ProductionAccounting.Services
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly EmployeeViewModel _employeeViewModel;
        private readonly CoefficientViewModel _coefficientViewModel;
        private readonly OperationsViewModel _operationsViewModel;
        private readonly ProductsViewModel _productsViewModel;
        private readonly WorkDataViewModel _workDataViewModel;
        private readonly CalculateSalaryViewModel _caclSalaryViewModel;
        private readonly InsertDataViewModel _insertDataViewModel;
        public ViewModelFactory(
            EmployeeViewModel employeeViewModel,
            CoefficientViewModel coefficientViewModel,
            OperationsViewModel operationsViewModel,
            ProductsViewModel productsViewModel,
            WorkDataViewModel workDataViewModel,
            CalculateSalaryViewModel caclSalaryViewModel,
            InsertDataViewModel insertDataViewModel
            )
        {
            _employeeViewModel = employeeViewModel;
            _coefficientViewModel = coefficientViewModel;
            _operationsViewModel = operationsViewModel;
            _productsViewModel = productsViewModel;
            _workDataViewModel = workDataViewModel;
            _caclSalaryViewModel = caclSalaryViewModel;
            _insertDataViewModel = insertDataViewModel;

        }
        public ViewModel CreateViewModel(int viewModelCode)
        {
            switch (viewModelCode)
            {
            case 0: return _employeeViewModel;
            case 1: return _coefficientViewModel;
            case 2: return _operationsViewModel;
            case 3: return _productsViewModel;
            case 4: return _workDataViewModel;
            case 5: return _caclSalaryViewModel;
            case 6: return _insertDataViewModel;
                default: return null;
            }
        }
    }
}
