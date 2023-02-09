using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System;
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


        private string? _title = "Test string";

        public string? Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        public MainWindowViewModel(
            IRepository<Employee> employees,
            IRepository<ExecutedOperation> executedOperation,
            IRepository<OperationСoefficient> operationСoefficient,
            IRepository<Operation> operation,
            IRepository<Product> product,
            IUserDialog<EmployeeModel> userDialog
            )
        {
            _employees = employees;
            _executedOperation = executedOperation;
            _operation = operation;
            _product = product;
            _operationСoefficient = operationСoefficient;
            _userDialog = userDialog;
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

        private ICommand _showEmployeeViewCommand;

        public ICommand ShowEmployeeViewCommand => _showEmployeeViewCommand ??= new LambdaCommand(OnShowEmployeeViewCommandExecuted, CanShowEmployeeViewCommandExecute);

        private bool CanShowEmployeeViewCommandExecute() => true;

        private void OnShowEmployeeViewCommandExecuted()
        {
            CurrentViewModel = new EmployeeViewModel(_employees, _userDialog);
        }

        private ICommand _showOperationsViewCommand;

        public ICommand ShowOperationsViewCommand => _showOperationsViewCommand ??= new LambdaCommand(OnShowOperationsViewCommandExecuted, CanShowOperationsViewCommandExecute);

        private bool CanShowOperationsViewCommandExecute() => true;

        private void OnShowOperationsViewCommandExecuted()
        {
            CurrentViewModel = new OperationViewModel();
        }
    }
}
