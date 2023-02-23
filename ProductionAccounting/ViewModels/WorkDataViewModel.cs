using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class WorkDataViewModel : ViewModel
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Operation> _operationRepository;
        private readonly IRepository<ExecutedOperation> _execRepository;

        public WorkDataViewModel(IRepository<ExecutedOperation> execRepository)
        {
            _execRepository = execRepository;
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

        #region Команда загрузки сотрудников
        private ICommand _getExecutedOperationsViewCommand;

        public ICommand GetExecutedOperationsViewCommand => _getExecutedOperationsViewCommand ??= new LambdaCommand(GetExecutedOperationsViewCommandExecuted, GetExecutedOperationsViewCommandExecute);

        private bool GetExecutedOperationsViewCommandExecute() => true;

        private async void GetExecutedOperationsViewCommandExecuted()
        {
            OnLoading = true;

            var task = Task.Run(() =>
            {
                return _execRepository.Items.ToList().Select(e => new ExecutedOperationDataGridModel(e));
            });
            var executedOperations = await task;
            ExecutedOperation = executedOperations.ToObservableCollection();
            OperationView = CollectionViewSource.GetDefaultView(ExecutedOperation);
            OperationView.Filter += _dataView_Filter;
            OnLoading = false;
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

        private int _monthFilter = DateTime.Now.Month;
        public int MonthFilter
        {
            get { return _monthFilter; }
            set
            {
                Set(ref _monthFilter, value);
                OperationView.Refresh();
            }
        }

        private int _yearFilter = DateTime.Now.Year;
        public int YearFilter
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
                && model.Year.ToString().Contains(YearFilter.ToString(), StringComparison.OrdinalIgnoreCase)
                && model.Month.ToString().Contains(MonthFilter.ToString(), StringComparison.OrdinalIgnoreCase)
                ;
            }
            return false;
        }
    }
}
