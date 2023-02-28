using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using ProductionAccounting.ViewModels.Base;
using System.Text.RegularExpressions;

namespace ProductionAccounting.ViewModels
{
    public class OperationEditorViewModel : ValidationViewModel
    {
        private readonly IRepository<OperationСoefficient> _repository;
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
                    var regex = new Regex(@"^(\s*[a-zA-Zа-яёЁА-Я-\s\d+]+)$");
                    if (!regex.IsMatch(value))
                    {
                        AddError(nameof(Name), "Название должно представлять из себя несколько слов только из букв");
                    }

                }
                Set(ref _name, value);
            }
        }

        private int _operationDuration;
        public int OperationDuration
        {
            get { return _operationDuration; }
            set
            {
                Set(ref _operationDuration, value);
            }
        }

        private string _operationDurationString;
        public string OperationDurationString
        {
            get { return _operationDurationString; }
            set
            {
                int tempint = 0;
                ClearErrors(nameof(OperationDurationString));
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(OperationDurationString), "Поле должно быть заполнено");
                }
                else
                {
                    if (int.TryParse(value, out tempint))
                    {
                        if (tempint < 0)
                        {
                            AddError(nameof(OperationDurationString), "Число должно быть положительным");
                        }
                    }
                    else
                    {
                        AddError(nameof(OperationDurationString), "Поле должно представлять из положительное число");
                    }
                    
                }
                OperationDuration = tempint;
                Set(ref _operationDurationString, value);
            }
        }

        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set
            {
                Set(ref _cost, value);
            }
        }

        private string _costString;
        public string CostString
        {
            get { return _costString; }
            set
            {
                decimal temp = 0;
                ClearErrors(nameof(CostString));
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(CostString), "Поле должно быть заполнено");
                }
                else
                {
                    if (decimal.TryParse(value, out temp))
                    {
                        if (temp < 0)
                        {
                            AddError(nameof(CostString), "Число должно быть положительным");
                        }
                    }
                    else
                    {
                        AddError(nameof(CostString), "Поле должно представлять из себя числовое значение");
                    }

                }
                Cost = temp;
                Set(ref _costString, value);
            }
        }

        private CoefficientModel _coefficient;
        public CoefficientModel Coefficient
        {
            get { return _coefficient; }
            set
            {
                ClearErrors(nameof(Coefficient));
                if (value == null)
                {
                    AddError(nameof(Coefficient), "Коэффициент должен быть выбран");
                }
                Set(ref _coefficient, value);
            }
        }

        private ObservableCollection<CoefficientModel> _coefficients;
        public ObservableCollection<CoefficientModel> Coefficients
        {
            get { return _coefficients; }
            set
            {
                Set(ref _coefficients, value);
            }
        }

        #region Команда загрузки коэффов
        private ICommand _getCoefficient;

        public ICommand GetCoefficient => _getCoefficient ??= new LambdaCommand(GetCoefficientsExecuted, GetCoefficientsExecute);

        private bool GetCoefficientsExecute() => true;

        private async void GetCoefficientsExecuted()
        {
            var task = Task.Run(() =>
            {
                return _repository.Items.ToList().Select(e => new CoefficientModel(e));
            });
            var coeffs = await task;
            Coefficients = coeffs.ToObservableCollection();
            Coefficient = Coefficients.FirstOrDefault(i => i.Id == Coefficient.Id);
        }
        #endregion

        public OperationEditorViewModel(OperationModel operation, IRepository<OperationСoefficient> repository)
        {
            Name = operation.Name;
            Cost = operation.Cost;
            CostString = Cost.ToString();
            OperationDuration = operation.OperationDuration;
            OperationDurationString = OperationDuration.ToString();
            Coefficient = (CoefficientModel)operation.Coefficient?.Clone() ?? new CoefficientModel();
            _repository = repository;
        }
    }
}
