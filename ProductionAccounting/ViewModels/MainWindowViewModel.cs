using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services;
using ProductionAccounting.Services.Interfaces;
using System;
using System.Reflection;
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
        private readonly IUserPrintDialog _printDialog;
        private readonly IChangeSaveFolderService _changeSaveFolderService;
        private readonly IAddingJobDataService _addingJobDataService;
        private readonly ICalculateSalaryService _salaryService;
        private readonly IConfirmDeleteDialog _confirmDeleteDialog;


        public MainWindowViewModel(
            IRepository<Employee> employees,
            IRepository<ExecutedOperation> executedOperation,
            IRepository<OperationСoefficient> operationСoefficient,
            IRepository<Operation> operation,
            IRepository<Product> product,
            IUserDialog<EmployeeModel> userDialog,
            IUserDialog<CoefficientModel> coeffDialog,
            IUserDialogWithRepository<OperationModel, OperationСoefficient> operationDialog,
            IUserDialogWithRepository<ProductModel, Operation> productDialog,
            IUserPrintDialog printDialog,
            IChangeSaveFolderService changeSaveFolderService,
            IAddingJobDataService addingJobDataService,
            ICalculateSalaryService salaryService,
            IConfirmDeleteDialog confirmDeleteDialog
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
            _printDialog = printDialog;
            _changeSaveFolderService = changeSaveFolderService;
            _addingJobDataService = addingJobDataService;
            _salaryService = salaryService;
            _confirmDeleteDialog = confirmDeleteDialog;

            IsLoaded = true;
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

        public Type CurrentViewModelType { get; set; }

        public bool IsLoaded { get; set; }

        #region Команда открытия вьюхи сотрудников
        private ICommand _showEmployeeViewCommand;

        public ICommand ShowEmployeeViewCommand => _showEmployeeViewCommand ??= new LambdaCommand(OnShowEmployeeViewCommandExecuted, CanShowEmployeeViewCommandExecute);

        private bool CanShowEmployeeViewCommandExecute() => IsLoaded;

        private void OnShowEmployeeViewCommandExecuted()
        {
            if (CurrentViewModel != null)
            {
                CurrentViewModel.PropertyChanged -= CurrentViewModel_PropertyChanged;
            }

            CurrentViewModel = new EmployeeViewModel(_employees, _userDialog);
            CurrentViewModelType = CurrentViewModel.GetType();
            CurrentViewModel.PropertyChanged += CurrentViewModel_PropertyChanged;
        }
        #endregion

        #region Команда открытия вьюхи изделий
        private ICommand _showProductsViewCommand;

        public ICommand ShowProductsViewCommand => _showProductsViewCommand ??= new LambdaCommand(OnShowProductsViewCommandExecuted, CanShowProductsViewCommandExecute);

        private bool CanShowProductsViewCommandExecute() => IsLoaded;

        private void OnShowProductsViewCommandExecuted()
        {
            if (CurrentViewModel != null)
            {
                CurrentViewModel.PropertyChanged -= CurrentViewModel_PropertyChanged;
            }
            CurrentViewModel = new ProductsViewModel(_operation, _product, _productDialog, _printDialog, _changeSaveFolderService);
            CurrentViewModelType = CurrentViewModel.GetType();
            CurrentViewModel.PropertyChanged += CurrentViewModel_PropertyChanged;
        }
        #endregion

        #region Команда открытия вьюхи коэффициентов
        private ICommand _showCoefficientViewCommand;

        public ICommand ShowCoefficientViewCommand => _showCoefficientViewCommand ??= new LambdaCommand(OnShowCoefficientViewCommandExecuted, CanShowCoefficientViewCommandExecute);

        private bool CanShowCoefficientViewCommandExecute() => IsLoaded;

        private void OnShowCoefficientViewCommandExecuted()
        {
            if (CurrentViewModel != null)
            {
                CurrentViewModel.PropertyChanged -= CurrentViewModel_PropertyChanged;
            }
            CurrentViewModel = new CoefficientViewModel(_operationСoefficient, _coeffDialog);
            CurrentViewModelType = CurrentViewModel.GetType();
            CurrentViewModel.PropertyChanged += CurrentViewModel_PropertyChanged;
        }
        #endregion

        #region Команда открытия вьюхи операций
        private ICommand _showOperationViewCommand;

        public ICommand ShowOperationViewCommand => _showOperationViewCommand ??= new LambdaCommand(OnShowOperationViewCommandExecuted, CanShowOperationViewCommandExecute);

        private bool CanShowOperationViewCommandExecute() => IsLoaded;

        private void OnShowOperationViewCommandExecuted()
        {
            if (CurrentViewModel != null)
            {
                CurrentViewModel.PropertyChanged -= CurrentViewModel_PropertyChanged;
            }
            
            CurrentViewModel = new OperationsViewModel(_operation, _operationСoefficient,  _operationDialog);
            CurrentViewModelType = CurrentViewModel.GetType();
            CurrentViewModel.PropertyChanged += CurrentViewModel_PropertyChanged;
        }
        #endregion

        #region Команда открытия вьюхи добавления данных
        private ICommand _showInsertDataViewCommand;

        public ICommand ShowInsertDataViewCommand => _showInsertDataViewCommand ??= new LambdaCommand(ShowInsertDataViewCommandExecuted, ShowInsertDataViewCommandExecute);

        private bool ShowInsertDataViewCommandExecute() => IsLoaded;

        private void ShowInsertDataViewCommandExecuted()
        {
            if (CurrentViewModel != null)
            {
                CurrentViewModel.PropertyChanged -= CurrentViewModel_PropertyChanged;
            }

            CurrentViewModel = new InsertDataViewModel(_operation, _employees, _addingJobDataService);
            CurrentViewModelType = CurrentViewModel.GetType();
            CurrentViewModel.PropertyChanged += CurrentViewModel_PropertyChanged;
        }
        #endregion

        #region Команда открытия вьюхи расчет зарплат
        private ICommand _showCalculateSalaryViewCommand;

        public ICommand ShowCalculateSalaryViewCommand => _showCalculateSalaryViewCommand ??= new LambdaCommand(ShowCalculateSalaryViewCommandExecuted, ShowCalculateSalaryViewCommandExecute);

        private bool ShowCalculateSalaryViewCommandExecute() => IsLoaded;

        private void ShowCalculateSalaryViewCommandExecuted()
        {
            if (CurrentViewModel != null)
            {
                CurrentViewModel.PropertyChanged -= CurrentViewModel_PropertyChanged;
            }

            CurrentViewModel = new CalculateSalaryViewModel(_salaryService, _employees);
            CurrentViewModelType = CurrentViewModel.GetType();
            CurrentViewModel.PropertyChanged += CurrentViewModel_PropertyChanged;
        }
        #endregion


        #region Команда открытия вьюхи расчет зарплат
        private ICommand _showWorkDataViewCommand;

        public ICommand ShowWorkDataViewCommand => _showWorkDataViewCommand ??= new LambdaCommand(ShowWorkDataViewCommandExecuted, ShowWorkDataViewCommandExecute);

        private bool ShowWorkDataViewCommandExecute() => IsLoaded;

        private void ShowWorkDataViewCommandExecuted()
        {
            if (CurrentViewModel != null)
            {
                CurrentViewModel.PropertyChanged -= CurrentViewModel_PropertyChanged;
            }

            CurrentViewModel = new WorkDataViewModel(_executedOperation, _confirmDeleteDialog);
            CurrentViewModelType = CurrentViewModel.GetType();
            CurrentViewModel.PropertyChanged += CurrentViewModel_PropertyChanged;
        }
        #endregion

        private void CurrentViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var model = Convert.ChangeType(sender, CurrentViewModelType);
            if (e.PropertyName == "OnLoading")
            {
                var props = model.GetType().GetProperties();
                PropertyInfo p = null;
                foreach (var property in props)
                {
                    if (property.Name == e.PropertyName)
                    {
                        p = property;
                        break;
                    }
                }
                IsLoaded = !(bool)p.GetValue(model);
            }
        }
    }
}
