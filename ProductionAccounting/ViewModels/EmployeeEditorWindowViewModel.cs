using MathCore.WPF.ViewModels;
using ProductionAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ProductionAccounting.ViewModels
{
    public class EmployeeEditorWindowViewModel : ViewModel, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                ClearErrors(nameof(Name));
                if (value != null && value.Contains("w"))
                {
                    AddError(nameof(Name), "Тест валидации");
                }
                Set(ref _name, value);
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                Set(ref _surname, value);
            }
        }

        private string _patronymic;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                Set(ref _patronymic, value);
            }
        }

        public bool HasErrors => _propertyErrors.Any();

        public EmployeeEditorWindowViewModel(EmployeeModel employee)
        {
            Name = employee.Name;
            Surname = employee.Surname;
            Patronymic = employee.Patronymic;
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName, null);
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            { 
                OnErrorsChanged(propertyName);
            }
        }
    }
}
