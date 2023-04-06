using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services;
using ProductionAccounting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class InsertDataViewModel : ViewModel
    {
        private readonly IRepository<Operation> _operationRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IAddingJobDataService _jobDataService;
        private readonly IShowExceptionDialogService _showExceptionDialog;
        private readonly AppStateService _appStateService;


        private bool _onLoading;
        public bool OnLoading
        {
            get { return _onLoading; }
            private set
            {
                Set(ref _onLoading, value);
            }
        }

        private int? _numberOfOperations;
        public int? NumberOfOperations
        {
            get { return _numberOfOperations; }
            set
            {
                Set(ref _numberOfOperations, value);
            }
        }

        private int _currentYear;
        public int CurrentYear
        {
            get { return _currentYear; }
            set
            {
                Set(ref _currentYear, value);
                UpdateData();
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
                UpdateData();
            }
        }

        private int _newNumberOfOperations;
        public int NewNumberOfOperations
        {
            get { return _newNumberOfOperations; }
            set
            {
                Set(ref _newNumberOfOperations, value);
            }
        }

        private List<OperationModel> _operationList;
        public List<OperationModel> OperationList
        {
            get { return _operationList; }
            set
            {
                Set(ref _operationList, value);
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
                UpdateData();
            }
        }

        private OperationModel _selectedOperation;
        public OperationModel SelectedOperation
        {
            get { return _selectedOperation; }
            set
            {
                Set(ref _selectedOperation, value);
                UpdateData();
            }
        }

        private void UpdateData()
        {
            if (SelectedOperation == null || SelectedEmployee == null)
            {
                NumberOfOperations = null;
                return;
            }
            NumberOfOperations = _jobDataService.GetNumberExecutedOperations(SelectedEmployee.Id, SelectedOperation.Id, CurrentMonth, CurrentYear);
        }

        public InsertDataViewModel(IRepository<Operation> operationRepository, IRepository<Employee> employeeRepository, IAddingJobDataService addingJobDataService, IShowExceptionDialogService showExceptionDialog, AppStateService appStateService, IRepository<Product> productRepository)
        {
            _jobDataService = addingJobDataService;
            Monthes = Enumerable.Range(1, 12).ToArray();
            CurrentYear = DateTime.Now.Year;
            CurrentMonth = DateTime.Now.Month;
            _operationRepository = operationRepository;
            _employeeRepository = employeeRepository;
            _showExceptionDialog = showExceptionDialog;
            _appStateService = appStateService;
            _productRepository = productRepository;

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
                if (_appStateService.SelectedProductModel != null)
                {
                    OperationList = _productRepository.Items.FirstOrDefault(p => p.Id == _appStateService.SelectedProductModel.Id).Operations.Select(o => new OperationModel(o)).ToList();
                }
                else
                {
                    var task = Task.Run(() =>
                    {
                        return _operationRepository.Items.ToList().Select(e => new OperationModel(e));
                    });
                    var operations = await task;
                    OperationList = operations.ToList();
                }
                var getEmployeeTask = Task.Run(() =>
                {
                    return _employeeRepository.Items.ToList().Select(e => new EmployeeModel(e));
                });
                var employees = await getEmployeeTask;
                EmployeeList = employees.ToList();
                OnLoading = false;
                SelectedEmployee = EmployeeList.FirstOrDefault();
                SelectedOperation = OperationList.FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
            finally
            {
                OnLoading = false;
            }

        }
        #endregion

        #region Команда добавления данных
        private ICommand _insertData;

        public ICommand InsertData => _insertData ??= new LambdaCommand(InsertDataExecuted, InsertDataExecute);

        private bool InsertDataExecute() => !OnLoading && SelectedEmployee != null && SelectedOperation != null;

        private async void InsertDataExecuted()
        {
            OnLoading = true;
            try
            {
                if (SelectedOperation == null || SelectedEmployee == null)
                {
                    NumberOfOperations = null;
                    return;
                }
                await _jobDataService.InsertData(SelectedEmployee.Id, SelectedOperation.Id, CurrentMonth, CurrentYear, NewNumberOfOperations);
                UpdateData();
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
            finally
            {
                OnLoading = false;
            }            
        }
        #endregion

        #region Команда открытия калькулятора
        private ICommand _openCalculator;

        public ICommand OpenCalculator => _openCalculator ??= new LambdaCommand(OpenCalculatorExecuted, OpenCalculatorExecute);

        private bool OpenCalculatorExecute() => true;

        private async void OpenCalculatorExecuted()
        {
            try
            {
                Process.Start("calc.exe");
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
        }
        #endregion
    }
}
