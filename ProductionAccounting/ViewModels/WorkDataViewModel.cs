using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class WorkDataViewModel : ViewModel
    {
        private readonly IRepository<ExecutedOperation> _execRepository;
        private readonly IConfirmDeleteDialog _confirmDeleteDialog;
        private readonly IShowExceptionDialogService _showExceptionDialog;

        public WorkDataViewModel(IRepository<ExecutedOperation> execRepository, IConfirmDeleteDialog confirmDeleteDialog, IShowExceptionDialogService showExceptionDialog)
        {
            _execRepository = execRepository;
            _confirmDeleteDialog = confirmDeleteDialog;
            _showExceptionDialog = showExceptionDialog;
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

        private ExecutedOperationDataGridModel _selectedItem;
        public ExecutedOperationDataGridModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
            }
        }

        #region Команда загрузки данных
        private ICommand _getExecutedOperationsViewCommand;

        public ICommand GetExecutedOperationsViewCommand => _getExecutedOperationsViewCommand ??= new LambdaCommand(GetExecutedOperationsViewCommandExecuted, GetExecutedOperationsViewCommandExecute);

        private bool GetExecutedOperationsViewCommandExecute() => true;

        private async void GetExecutedOperationsViewCommandExecuted()
        {
            OnLoading = true;
            try
            {
                var task = Task.Run(() =>
                {
                    return _execRepository.Items.ToList().Select(e => new ExecutedOperationDataGridModel(e));
                });
                var executedOperations = await task;
                ExecutedOperation = executedOperations.ToObservableCollection();
                OperationView = CollectionViewSource.GetDefaultView(ExecutedOperation);
                OperationView.Filter += _dataView_Filter;
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

        #region Команда удаления записей
        private ICommand _deleteExecutedOperationsViewCommand;

        public ICommand DeleteExecutedOperationsViewCommand => _deleteExecutedOperationsViewCommand ??= new LambdaCommand(DeleteExecutedOperationsViewCommandExecuted, DeleteExecutedOperationsViewCommandExecute);

        private bool DeleteExecutedOperationsViewCommandExecute() => SelectedItem != null && !OnLoading;

        private async void DeleteExecutedOperationsViewCommandExecuted()
        {
            try
            {
                if (!_confirmDeleteDialog.ShowConfirmDeleteDialog("Удалить эту запись?")) return;
                OnLoading = true;
                await Task.Run(async () =>
                {
                    var deleteItem = await _execRepository.Items.FirstOrDefaultAsync(i => i.Id == SelectedItem.Id);
                    if (deleteItem != null)
                    {
                        await _execRepository.DeleteAsync(deleteItem.Id);
                        await _execRepository.SaveChangesAsync();
                    }
                });
                ExecutedOperation.Remove(SelectedItem);
                SelectedItem = null;
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

        private string _nameFilter;
        public string NameFilter
        {
            get { return _nameFilter; }
            set
            {
                Set(ref _nameFilter, value);
                OperationView.Refresh();
            }
        }

        private string _monthFilter = DateTime.Now.Month.ToString();
        public string MonthFilter
        {
            get { return _monthFilter; }
            set
            {
                Set(ref _monthFilter, value);
                OperationView.Refresh();
            }
        }

        private string _yearFilter = DateTime.Now.Year.ToString();
        public string YearFilter
        {
            get { return _yearFilter; }
            set
            {
                Set(ref _yearFilter, value);
                OperationView.Refresh();
            }
        }

        private ObservableCollection<ExecutedOperationDataGridModel> _executedOperation = new ObservableCollection<ExecutedOperationDataGridModel>();

        public ObservableCollection<ExecutedOperationDataGridModel> ExecutedOperation
        {
            get { return _executedOperation; }
            set
            {
                Set(ref _executedOperation, value);
            }
        }

        private ICollectionView _operationView;
        public ICollectionView OperationView
        {
            get { return _operationView; }
            set
            {
                Set(ref _operationView, value);
            }
        }        

        private bool _dataView_Filter(object sender)
        {
            if (sender is ExecutedOperationDataGridModel model)
            {
                return model.Employee.ToString().Contains(NameFilter ?? String.Empty, StringComparison.OrdinalIgnoreCase)
                && model.Year.ToString().Contains(YearFilter ?? String.Empty, StringComparison.OrdinalIgnoreCase)
                && model.Month.ToString().Contains(MonthFilter ?? String.Empty, StringComparison.OrdinalIgnoreCase)
                ;
            }
            return false;
        }
    }
}
