using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class EmployeeViewModel : ViewModel
    {
        private readonly IRepository<Employee> _employeeRepository;
        public EmployeeViewModel(IRepository<Employee> repository)
        {
            _employeeRepository = repository;
        }

        private ObservableCollection<EmployeeModel> _employees;

        public ObservableCollection<EmployeeModel> Employees
        {
            get { return _employees; }
            private set
            {
                Set(ref _employees, value);
            }
        }

        private ICommand _getEmployeesViewCommand;

        public ICommand GetEmployeesViewCommand => _getEmployeesViewCommand ??= new LambdaCommand(GetEmployeesViewCommandExecuted, GetEmployeesViewCommandExecute);

        private bool GetEmployeesViewCommandExecute() => true;

        private async void GetEmployeesViewCommandExecuted()
        {
            
            var task = Task.Run(() =>
            {
                return _employeeRepository.Items.ToList().Select(e => new EmployeeModel().MapToDto(e));
            });
            var employees = await task;
            Employees = employees.ToObservableCollection();
        }
    }
}
