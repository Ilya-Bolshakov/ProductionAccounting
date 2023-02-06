using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private string? _title = "Test string";

        public string? Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private ViewModel _currentViewModel;

        public ViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            private set { Set(ref _currentViewModel, value); }
        }

        private ICommand _showEmployeeViewCommand;

        public ICommand ShowEmployeeViewCommand => _showEmployeeViewCommand ??= new LambdaCommand(OnShowEmployeeViewCommandExecuted, CanShowEmployeeViewCommandExecute);

        private bool CanShowEmployeeViewCommandExecute() => true;

        private void OnShowEmployeeViewCommandExecuted()
        {
            CurrentViewModel = new EmployeeViewModel();
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
