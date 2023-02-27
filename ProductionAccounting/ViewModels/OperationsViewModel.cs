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
    public class OperationsViewModel : ViewModel
    {
        private readonly IRepository<Operation> _operationRepository;
        private readonly IRepository<OperationСoefficient> _operationCoefficient;
        private readonly IUserDialogWithRepository<OperationModel, OperationСoefficient> _userDialogWithRepo;
        public OperationsViewModel(IRepository<Operation> repository, IRepository<OperationСoefficient> coefficientRepository, IUserDialogWithRepository<OperationModel, OperationСoefficient> userDialog)
        {
            _operationRepository = repository;
            _userDialogWithRepo = userDialog;
            _operationCoefficient = coefficientRepository;
        }

        public OperationsViewModel()
        { }

        private ObservableCollection<OperationModel> _operations;

        public ObservableCollection<OperationModel> Operations
        {
            get { return _operations; }
            private set
            {
                Set(ref _operations, value);
            }
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

        private OperationModel _selectedItem;
        public OperationModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
            }
        }

        #region Команда загрузки операций
        private ICommand _getOperations;

        public ICommand GetOperations => _getOperations ??= new LambdaCommand(GetOperationsExecuted, GetOperationsExecute);

        private bool GetOperationsExecute() => !OnLoading;

        private async void GetOperationsExecuted()
        {
            OnLoading = true;
            var task = Task.Run(() =>
            {
                return _operationRepository.Items.ToList().Select(e => new OperationModel(e));
            });
            var operations = await task;
            Operations = operations.ToObservableCollection();
            OnLoading = false;
        }
        #endregion

        #region Команда добавления операций
        private ICommand _addOperations;

        public ICommand AddOperations => _addOperations ??= new LambdaCommand(AddOperationsExecuted, AddOperationsExecute);

        private bool AddOperationsExecute() => true;

        private async void AddOperationsExecuted()
        {
            OperationModel operation = new();
            if (!_userDialogWithRepo.Edit(operation, _operationCoefficient))
            {
                return;
            }
            Operations.Add(operation);
            var entity = operation.MapToOrm();
            _operationRepository.Add(entity);
            await _operationRepository.SaveChangesAsync();
            operation.Id = entity.Id;
            SelectedItem = operation;

        }
        #endregion

        #region Команда редактирования операций
        private ICommand _editOperations;

        public ICommand EditOperations => _editOperations ??= new LambdaCommand(EditOperationsExecuted, EditOperationsExecute);

        private bool EditOperationsExecute() => SelectedItem != null;

        private async void EditOperationsExecuted()
        {
            if (!_userDialogWithRepo.Edit(SelectedItem, _operationCoefficient))
            {
                return;
            }
            var updateop = _operationRepository.GetById(SelectedItem.Id);
            updateop.Name = SelectedItem.Name;
            updateop.Cost = SelectedItem.Cost;
            updateop.OperationDuration = SelectedItem.OperationDuration;
            updateop.OperationСoefficient = _operationCoefficient.GetById(SelectedItem.Coefficient.Id);
            _operationRepository.Update(updateop);
            await _operationRepository.SaveChangesAsync();
        }
        #endregion

        #region Команда удаления операций
        private ICommand _deleteOperations;

        public ICommand DeleteOperations => _deleteOperations ??= new LambdaCommand(DeleteOperationsExecuted, DeleteOperationsExecute);

        private bool DeleteOperationsExecute() => SelectedItem != null;

        private async void DeleteOperationsExecuted()
        {
            var removeModel = SelectedItem;
            if (!_userDialogWithRepo.ConfirmOperation("Вы действительно хотите удалить этого сотрудника?", "Удаление сотрудника")) return;
            Operations.Remove(removeModel);
            await _operationRepository.DeleteAsync(removeModel.Id);
            await _operationRepository.SaveChangesAsync();
            if (ReferenceEquals(SelectedItem, removeModel)) SelectedItem = null;
        }
        #endregion
    }
}
