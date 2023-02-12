﻿using MathCore.WPF.Commands;
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
        private readonly IUserDialog<OperationModel> _userDialog;
        public OperationsViewModel(IRepository<Operation> repository, IUserDialog<OperationModel> userDialog)
        {
            _operationRepository = repository;
            _userDialog = userDialog;
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

        private Visibility _onLoading;
        public Visibility OnLoading
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

        #region Команда загрузки сотрудников
        private ICommand _getOperations;

        public ICommand GetOperations => _getOperations ??= new LambdaCommand(GetOperationsExecuted, GetOperationsExecute);

        private bool GetOperationsExecute() => true;

        private async void GetOperationsExecuted()
        {
            OnLoading = Visibility.Visible;
            var task = Task.Run(() =>
            {
                return _operationRepository.Items.ToList().Select(e => new OperationModel(e));
            });
            var operations = await task;
            Operations = operations.ToObservableCollection();
            OnLoading = Visibility.Hidden;
        }
        #endregion

        #region Команда добавления сотрудников
        private ICommand _addOperations;

        public ICommand AddOperations => _addOperations ??= new LambdaCommand(AddOperationsExecuted, AddOperationsExecute);

        private bool AddOperationsExecute() => true;

        private async void AddOperationsExecuted()
        {
            OperationModel operation = new();
            if (!_userDialog.Edit(operation))
            {
                return;
            }
            Operations.Add(operation);
            _operationRepository.Add(operation.MapToOrm());
            await _operationRepository.SaveChangesAsync();

            SelectedItem = operation;

        }
        #endregion

        #region Команда редактирования сотрудников
        private ICommand _editOperations;

        public ICommand EditOperations => _editOperations ??= new LambdaCommand(EditOperationsExecuted, EditOperationsExecute);

        private bool EditOperationsExecute() => SelectedItem != null;

        private async void EditOperationsExecuted()
        {
            if (!_userDialog.Edit(SelectedItem))
            {
                return;
            }
            var updateop = _operationRepository.GetById(SelectedItem.Id);
            updateop.Name = SelectedItem.Name;
            updateop.Cost = SelectedItem.Cost;
            updateop.OperationDuration = SelectedItem.OperationDuration;
            updateop.OperationСoefficient = SelectedItem.Coefficient.MapToOrm();
            _operationRepository.Update(updateop);
            await _operationRepository.SaveChangesAsync();

        }
        #endregion

        #region Команда удаления сотрудников
        private ICommand _deleteOperations;

        public ICommand DeleteOperations => _deleteOperations ??= new LambdaCommand(DeleteOperationsExecuted, DeleteOperationsExecute);

        private bool DeleteOperationsExecute() => SelectedItem != null;

        private async void DeleteOperationsExecuted()
        {
            var removeModel = SelectedItem;
            if (!_userDialog.ConfirmOperation("Вы действительно хотите удалить этого сотрудника?", "Удаление сотрудника")) return;
            Operations.Remove(removeModel);
            await _operationRepository.DeleteAsync(removeModel.Id);
            await _operationRepository.SaveChangesAsync();
            if (ReferenceEquals(SelectedItem, removeModel)) SelectedItem = null;
        }
        #endregion
    }
}