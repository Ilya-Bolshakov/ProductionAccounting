using MathCore.WPF.Commands;
using Microsoft.IdentityModel.Tokens;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Infrastructure.Commands;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class ProductEditorViewModel : ValidationViewModel
    {
        private readonly IRepository<Operation> _repository;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                ClearErrors(nameof(Name));
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(Name), "Название должно быть заполнено");
                }
                else
                {
                    var regex = new Regex(@"^(\s*[a-zA-Zа-яА-Я\s]+)$");
                    value = value.Trim();
                    if (!regex.IsMatch(value))
                    {
                        AddError(nameof(Name), "Название должно представлять из себя несколько слов только из букв");
                    }

                }
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

        #region Команда загрузки операций
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

        #region Команда редактирования изделия
        private ICommand _editProduct;

        public ICommand EditProduct => _editProduct ??= new LambdaCommand<IList<object>>(EditProductExecuted, EditProductExecute);

        private bool EditProductExecute() => true;

        private void EditProductExecuted(IList<object> operations)
        {
            var selOp = operations.Select(o => (OperationModel)o).ToList();
            SelectedOperations = selOp;
            var command = new DialogResultCommand();
            if (command.CanExecute(true))
            {
                command.Execute(true);
            }
            
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
