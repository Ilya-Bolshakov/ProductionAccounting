using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class EmployeeViewModel : ViewModel
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IUserDialog<EmployeeModel> _userDialog;

        public EmployeeViewModel()
        {

        }

        public EmployeeViewModel(IRepository<Employee> repository, IUserDialog<EmployeeModel> userDialog)
        {
            _employeeRepository = repository;
            _userDialog = userDialog;
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

        private Visibility _onLoading;
        public Visibility OnLoading
        {
            get { return _onLoading; }
            private set
            {
                Set(ref _onLoading, value);
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

        #region Команда загрузки сотрудников
        private ICommand _getEmployeesViewCommand;

        public ICommand GetEmployeesViewCommand => _getEmployeesViewCommand ??= new LambdaCommand(GetEmployeesViewCommandExecuted, GetEmployeesViewCommandExecute);

        private bool GetEmployeesViewCommandExecute() => true;

        private async void GetEmployeesViewCommandExecuted()
        {
            OnLoading = Visibility.Visible;
            var task = Task.Run(() =>
            {
                return _employeeRepository.Items.ToList().Select(e => new EmployeeModel(e));
            });
            var employees = await task;
            Employees = employees.ToObservableCollection();
            OnLoading = Visibility.Hidden;
        }
        #endregion

        #region Команда добавления сотрудников
        private ICommand _addEmployeeViewCommand;

        public ICommand AddEmployeeViewCommand => _addEmployeeViewCommand ??= new LambdaCommand(AddEmployeeViewCommandExecuted, AddEmployeeViewCommandExecute);

        private bool AddEmployeeViewCommandExecute() => true;

        private async void AddEmployeeViewCommandExecuted()
        {
            EmployeeModel employee = new();
            if (!_userDialog.Edit(employee))
            {
                return;
            }
            Employees.Add(employee);
            _employeeRepository.Add(employee.MapToOrm());
            await _employeeRepository.SaveChangesAsync();

            SelectedItem = employee;

        }
        #endregion

        #region Команда редактирования сотрудников
        private ICommand _editEmployeeViewCommand;

        public ICommand EditEmployeeViewCommand => _editEmployeeViewCommand ??= new LambdaCommand(EditEmployeeViewCommandExecuted, EditEmployeeViewCommandExecute);

        private bool EditEmployeeViewCommandExecute() => SelectedItem != null;

        private async void EditEmployeeViewCommandExecuted()
        {
            if (!_userDialog.Edit(SelectedItem))
            {
                return;
            }
            var updateEmp = _employeeRepository.GetById(SelectedItem.Id);
            updateEmp.Name = SelectedItem.Name;
            updateEmp.Surname = SelectedItem.Surname;
            updateEmp.Patronymic = SelectedItem.Patronymic;
            _employeeRepository.Update(updateEmp);
            await _employeeRepository.SaveChangesAsync();

        }
        #endregion

        #region Команда удаления сотрудников
        private ICommand _deleteEmployeeViewCommand;

        public ICommand DeleteEmployeeViewCommand => _deleteEmployeeViewCommand ??= new LambdaCommand(DeleteEmployeeViewCommandExecuted, DeleteEmployeeViewCommandExecute);

        private bool DeleteEmployeeViewCommandExecute() => SelectedItem != null;

        private async void DeleteEmployeeViewCommandExecuted()
        {
            var removeModel = SelectedItem;
            if (!_userDialog.ConfirmOperation("Вы действительно хотите удалить этого сотрудника?", "Удаление сотрудника")) return;
            Employees.Remove(removeModel);
            await _employeeRepository.DeleteAsync(removeModel.Id);
            await _employeeRepository.SaveChangesAsync();
            if (ReferenceEquals(SelectedItem, removeModel)) SelectedItem = null;
        }
        #endregion
    }
}
