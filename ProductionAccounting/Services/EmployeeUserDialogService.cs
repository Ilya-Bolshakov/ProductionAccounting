using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class EmployeeUserDialogService : IUserDialog<EmployeeModel>
    {
        private readonly IShowExceptionDialogService _showExceptionDialogService;
        public EmployeeUserDialogService(IShowExceptionDialogService showExceptionDialogService)
        {
            _showExceptionDialogService = showExceptionDialogService;
        }
        public bool ConfirmOperation(string info, string caption)
        {
            return MessageBox.Show(info, 
                                   caption, 
                                   MessageBoxButton.YesNo, 
                                   MessageBoxImage.Warning, 
                                   MessageBoxResult.Yes) == MessageBoxResult.Yes;
        }

        public bool Edit(EmployeeModel employee)
        {
            try
            {
                var employeeVM = new EmployeeEditorWindowViewModel(employee);

                var employeeV = new EmployeeEditorWindow()
                {
                    DataContext = employeeVM,
                };

                if (employeeV.ShowDialog() != true) return false;

                employee.Name = employeeVM.Name;
                employee.Surname = employeeVM.Surname;
                employee.Patronymic = employeeVM.Patronymic;

                return true;
            }
            catch (System.Exception ex)
            {
                _showExceptionDialogService.ShowDialog("Ошибка при работе программы, попробуйте еще раз. Показать ошибку для разработчика?", ex.Message);
                return false;
            }
            
        }
    }
}
