using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class CalculateSalaryViewModel : ViewModel
    {
        private readonly ICalculateSalaryService _service;
        private readonly IRepository<Employee> _employeeRepository;
        public CalculateSalaryViewModel(ICalculateSalaryService service, IRepository<Employee> employeeRepository)
        {
            _service = service;
            _employeeRepository = employeeRepository;
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

        private List<EmployeeModel> _employeeList;
        public List<EmployeeModel> EmployeeList
        {
            get { return _employeeList; }
            set
            {
                Set(ref _employeeList, value);
            }
        }

        private EmployeeModel _selectedEmployee;
        public EmployeeModel SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                Set(ref _selectedEmployee, value);
            }
        }

        private int _currentYear;
        public int CurrentYear
        {
            get { return _currentYear; }
            set
            {
                Set(ref _currentYear, value);
            }
        }

        private int[] _monthes;
        public int[] Monthes
        {
            get { return _monthes; }
            set
            {
                Set(ref _monthes, value);
            }
        }

        private int _currentMonth;
        public int CurrentMonth
        {
            get { return _currentMonth; }
            set
            {
                Set(ref _currentMonth, value);
            }
        }

        private decimal _salary;
        public decimal Salary
        {
            get { return _salary; }
            set
            {
                Set(ref _salary, value);
            }
        }

        #region Команда загрузки всех данных
        private ICommand _getData;

        public ICommand GetData => _getData ??= new LambdaCommand(GetDataExecuted, GetDataExecute);

        private bool GetDataExecute() => !OnLoading;

        private async void GetDataExecuted()
        {
            OnLoading = true;
            var getEmployeeTask = Task.Run(() =>
            {
                return _employeeRepository.Items.ToList().Select(e => new EmployeeModel(e));
            });
            var employees = await getEmployeeTask;
            EmployeeList = employees.ToList();
            Monthes = Enumerable.Range(1, 12).ToArray();
            CurrentMonth = DateTime.Now.Month;
            CurrentYear = DateTime.Now.Year;
            OnLoading = false;
            SelectedEmployee = EmployeeList.FirstOrDefault();
        }
        #endregion


        #region Команда получения зп
        private ICommand _getSalary;

        public ICommand GetSalary => _getSalary ??= new LambdaCommand(GetSalaryExecuted, GetSalaryExecute);

        private bool GetSalaryExecute() => !OnLoading;

        private async void GetSalaryExecuted()
        {
            OnLoading = true;
            Salary = _service.CalculateEmployeeSalary(SelectedEmployee.Id, CurrentYear, CurrentMonth);
            OnLoading = false;
        }
        #endregion
    }
}
