using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;

namespace ProductionAccounting.Services
{
    public class UserDialogService : IUserDialog
    {
        public bool Edit(EmployeeModel employee)
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
    }
}
