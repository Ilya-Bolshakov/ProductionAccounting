using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class EmployeeUserDialogService : IUserDialog<EmployeeModel>
    {
        public EmployeeUserDialogService()
        {

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
            catch (System.Exception)
            {
                MessageBox.Show("Ошибка при работе программы, попробуйте еще раз",
                                                 "Ошибка",
                                                 MessageBoxButton.OK,
                                                 MessageBoxImage.Error,
                                                 MessageBoxResult.Yes);
                return false;
            }
            
        }
    }
}
