using ProductionAccounting.Models;
using ProductionAccounting.ViewModels.Base;
using System.Text.RegularExpressions;
using System;

namespace ProductionAccounting.ViewModels
{
    public class CoefficientEditorViewModel : ValidationViewModel
    {
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

        private decimal _coefficientValue;
        public decimal CoefficientValue
        {
            get { return _coefficientValue; }
            set
            {
                Set(ref _coefficientValue, value);
            }
        }

        private string _coefficientValueString;
        public string CoefficientValueString
        {
            get { return _coefficientValueString; }
            set
            {
                decimal temp = 0;
                ClearErrors(nameof(CoefficientValueString));
                if (String.IsNullOrEmpty(value))
                {
                    AddError(nameof(CoefficientValueString), "Поле должно быть заполнено");
                }
                else
                {
                    if (decimal.TryParse(value, out temp))
                    {
                        if (temp < 0)
                        {
                            AddError(nameof(CoefficientValueString), "Коэффициент должен быть положительным");
                        }
                    }
                    else
                    {
                        AddError(nameof(CoefficientValueString), "Коэффициент должен представлять из числовое значение");
                    }

                }
                CoefficientValue = temp;
                Set(ref _coefficientValueString, value);
                
            }
        }

        public CoefficientEditorViewModel()
        {

        }

        public CoefficientEditorViewModel(CoefficientModel coefficientModel)
        {
            Name = coefficientModel.Name;
            CoefficientValue = coefficientModel.CoefficientValue;
            CoefficientValueString = CoefficientValue.ToString();
        }


    }
}
