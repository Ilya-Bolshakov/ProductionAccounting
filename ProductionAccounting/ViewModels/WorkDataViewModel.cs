using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class WorkDataViewModel : ViewModel
    {
        private readonly IRepository<Employee> _employeeRepository;

        public WorkDataViewModel(IRepository<Employee> repository)
        {
            _employeeRepository = repository;
        }

        private bool _onLoading;
        public bool OnLoading
        {
            get { return _onLoading; }
            private set
            {
                Set(ref _onLoading, value);
            }
        }

        #region Команда загрузки сотрудников
        private ICommand _getEmployeesViewCommand;

        public ICommand GetEmployeesViewCommand => _getEmployeesViewCommand ??= new LambdaCommand(GetEmployeesViewCommandExecuted, GetEmployeesViewCommandExecute);

        private bool GetEmployeesViewCommandExecute() => true;

        private async void GetEmployeesViewCommandExecuted()
        {
            OnLoading = true;
            var task = Task.Run(() =>
            {
                return _employeeRepository.Items.ToList().Select(e => new EmployeeModel(e));
            });
            var employees = await task;
            var Employeese = employees.ToObservableCollection();
            OnLoading = false;
        }
        #endregion

    }
}
