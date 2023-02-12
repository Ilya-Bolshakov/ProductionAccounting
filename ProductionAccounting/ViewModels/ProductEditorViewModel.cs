using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class ProductEditorViewModel : ViewModel
    {
        private readonly IRepository<Operation> _repository;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
            }
        }

        private List<OperationModel> _operations;
        public List<OperationModel> Operations
        {
            get { return _operations; }
            set
            {
                Set(ref _operations, value);
            }
        }

        private List<OperationModel> _selectedOperations;
        public List<OperationModel> SelectedOperations
        {
            get { return _selectedOperations; }
            set
            {
                Set(ref _selectedOperations, value);
            }
        }

        #region Команда загрузки коэффов
        private ICommand _getOperations;

        public ICommand GetOperations => _getOperations ??= new LambdaCommand(GetCoefficientsExecuted, GetCoefficientsExecute);

        private bool GetCoefficientsExecute() => true;

        private async void GetCoefficientsExecuted()
        {
            var task = Task.Run(() =>
            {
                return _repository.Items.Select(o => new OperationModel(o)).ToList();
            });
            var operations = await task;
            Operations = operations;
        }
        #endregion

        public ProductEditorViewModel(IRepository<Operation> repository, ProductModel model)
        {
            _repository = repository; 
            Name = model.Name;
            SelectedOperations = model.Operations;
        }
    }
}
