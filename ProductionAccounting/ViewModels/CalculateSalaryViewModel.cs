using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class CalculateSalaryViewModel : ViewModel
    {
        private readonly ICalculateSalaryService _service;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IShowExceptionDialogService _showExceptionDialog;
        public CalculateSalaryViewModel(ICalculateSalaryService service, IRepository<Employee> employeeRepository, IShowExceptionDialogService showExceptionDialog)
        {
            _service = service;
            _employeeRepository = employeeRepository;
            _showExceptionDialog = showExceptionDialog;
        }

        private bool _onLoading;
        public bool OnLoading
        {
            get { return _onLoading; }
            private set
            {
                Set(ref _onLoading, value);
                IsEnable = !HasPickedAll && !OnLoading;
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

        private List<EmployeeModel> _selectedEmployee;
        public List<EmployeeModel> SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                Set(ref _selectedEmployee, value);
            }
        }

        private EmployeeModel _selectedItem;
        public EmployeeModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
            }
        }

        private ObservableCollection<EmployeeAndHisSalary> _employeeAndHisSalaries = new();
        public ObservableCollection<EmployeeAndHisSalary> EmployeeAndHisSalaries
        {
            get { return _employeeAndHisSalaries; }
            set
            {
                Set(ref _employeeAndHisSalaries, value);
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

        //private decimal _salary;
        //public decimal Salary
        //{
        //    get { return _salary; }
        //    set
        //    {
        //        Set(ref _salary, value);
        //    }
        //}

        private bool _hasPickedAll;
        public bool HasPickedAll
        {
            get { return _hasPickedAll; }
            set
            {
                Set(ref _hasPickedAll, value);
                IsEnable = !HasPickedAll && !OnLoading;
            }
        }

        private bool _isEnable;
        public bool IsEnable
        {
            get { return _isEnable; }
            set 
            {
                Set(ref _isEnable, value);
            }
        }

        #region Команда загрузки всех данных
        private ICommand _getData;

        public ICommand GetData => _getData ??= new LambdaCommand(GetDataExecuted, GetDataExecute);

        private bool GetDataExecute() => !OnLoading;

        private async void GetDataExecuted()
        {
            OnLoading = true;
            try
            {
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
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
            finally{
                OnLoading = false;
            }
        }
        #endregion


        #region Команда получения зп
        private ICommand _getSalary;

        public ICommand GetSalary => _getSalary ??= new LambdaCommand<IList<object>>(GetSalaryExecuted, GetSalaryExecute);

        private bool GetSalaryExecute() => !OnLoading && (SelectedItem != null || HasPickedAll);

        private async void GetSalaryExecuted(IList<object> employees)
        {
            OnLoading = true;
            var selEm = HasPickedAll ? EmployeeList : employees.Select(o => (EmployeeModel)o).ToList();
            EmployeeAndHisSalaries.Clear();
            foreach (var e in selEm)
            {
                var salaryTask = _service.CalculateEmployeeSalaryAsync(e.Id, CurrentYear, CurrentMonth);
                var salary = await salaryTask;
                EmployeeAndHisSalaries.Add(new EmployeeAndHisSalary() { Employee = e, Salary = salary });
            }
            OnLoading = false;
        }
        #endregion
    }
}
