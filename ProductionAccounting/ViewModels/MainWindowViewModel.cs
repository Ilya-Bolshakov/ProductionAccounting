using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.Enums;
using ProductionAccounting.Services.Interfaces;
using System;
using System.Reflection;
using System.Windows.Input;
using static ProductionAccounting.Enums.ViewEnum;

namespace ProductionAccounting.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;

        public MainWindowViewModel(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;

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

            CurrentViewModel = _viewModelFactory.CreateViewModel((int)EnumView.EmployeeViewModel);
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
            CurrentViewModel = _viewModelFactory.CreateViewModel((int)EnumView.ProductsViewModel);
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
            CurrentViewModel = _viewModelFactory.CreateViewModel((int)EnumView.CoefficientViewModel);
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

            CurrentViewModel = _viewModelFactory.CreateViewModel((int)EnumView.OperationsViewModel);
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

            CurrentViewModel = _viewModelFactory.CreateViewModel((int)EnumView.InsertDataViewModel);
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

            CurrentViewModel = _viewModelFactory.CreateViewModel((int)EnumView.CalculateSalaryViewModel);
            CurrentViewModelType = CurrentViewModel.GetType();
            CurrentViewModel.PropertyChanged += CurrentViewModel_PropertyChanged;
        }
        #endregion

        #region Команда открытия вьюхи с данными о работе всех
        private ICommand _showWorkDataViewCommand;

        public ICommand ShowWorkDataViewCommand => _showWorkDataViewCommand ??= new LambdaCommand(ShowWorkDataViewCommandExecuted, ShowWorkDataViewCommandExecute);

        private bool ShowWorkDataViewCommandExecute() => IsLoaded;

        private void ShowWorkDataViewCommandExecuted()
        {
            if (CurrentViewModel != null)
            {
                CurrentViewModel.PropertyChanged -= CurrentViewModel_PropertyChanged;
            }

            CurrentViewModel = _viewModelFactory.CreateViewModel((int)EnumView.WorkDataViewModel);
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
