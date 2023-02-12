using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Employee> _employees;
        private readonly IRepository<ExecutedOperation> _executedOperation;
        private readonly IRepository<OperationСoefficient> _operationСoefficient;
        private readonly IRepository<Operation> _operation;
        private readonly IRepository<Product> _product;
        private readonly IUserDialog<EmployeeModel> _userDialog;
        private readonly IUserDialog<CoefficientModel> _coeffDialog;
        private readonly IUserDialogWithRepository<OperationModel, OperationСoefficient> _operationDialog;
        private readonly IUserDialogWithRepository<ProductModel, Operation> _productDialog;

        public MainWindowViewModel(
            IRepository<Employee> employees,
            IRepository<ExecutedOperation> executedOperation,
            IRepository<OperationСoefficient> operationСoefficient,
            IRepository<Operation> operation,
            IRepository<Product> product,
            IUserDialog<EmployeeModel> userDialog,
            IUserDialog<CoefficientModel> coeffDialog,
            IUserDialogWithRepository<OperationModel, OperationСoefficient> operationDialog,
            IUserDialogWithRepository<ProductModel, Operation> productDialog
            )
        {
            _employees = employees;
            _executedOperation = executedOperation;
            _operation = operation;
            _product = product;
            _operationСoefficient = operationСoefficient;
            _userDialog = userDialog;
            _coeffDialog = coeffDialog;
            _operationDialog = operationDialog;
            _productDialog = productDialog;
        }

        private ViewModel _currentViewModel;

        public ViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            private set 
            {
                if (_currentViewModel?.GetType() != value?.GetType())
                    Set(ref _currentViewModel, value); 
            }
        }

        #region Команда открытия вьюхи сотрудников
        private ICommand _showEmployeeViewCommand;

        public ICommand ShowEmployeeViewCommand => _showEmployeeViewCommand ??= new LambdaCommand(OnShowEmployeeViewCommandExecuted, CanShowEmployeeViewCommandExecute);

        private bool CanShowEmployeeViewCommandExecute() => true;

        private void OnShowEmployeeViewCommandExecuted()
        {
            CurrentViewModel = new EmployeeViewModel(_employees, _userDialog);
        }
        #endregion

        #region Команда открытия вьюхи изделий
        private ICommand _showProductsViewCommand;

        public ICommand ShowProductsViewCommand => _showProductsViewCommand ??= new LambdaCommand(OnShowProductsViewCommandExecuted, CanShowProductsViewCommandExecute);

        private bool CanShowProductsViewCommandExecute() => true;

        private void OnShowProductsViewCommandExecuted()
        {
            CurrentViewModel = new ProductsViewModel(_operation, _product, _productDialog);
        }
        #endregion

        #region Команда открытия вьюхи коэффициентов
        private ICommand _showCoefficientViewCommand;

        public ICommand ShowCoefficientViewCommand => _showCoefficientViewCommand ??= new LambdaCommand(OnShowCoefficientViewCommandExecuted, CanShowCoefficientViewCommandExecute);

        private bool CanShowCoefficientViewCommandExecute() => true;

        private void OnShowCoefficientViewCommandExecuted()
        {
            CurrentViewModel = new CoefficientViewModel(_operationСoefficient, _coeffDialog);
        }
        #endregion

        #region Команда открытия вьюхи операций
        private ICommand _showOperationViewCommand;

        public ICommand ShowOperationViewCommand => _showOperationViewCommand ??= new LambdaCommand(OnShowOperationViewCommandExecuted, CanShowOperationViewCommandExecute);

        private bool CanShowOperationViewCommandExecute() => true;

        private void OnShowOperationViewCommandExecuted()
        {
            CurrentViewModel = new OperationsViewModel(_operation, _operationСoefficient,  _operationDialog);
        }
        #endregion
    }
}
