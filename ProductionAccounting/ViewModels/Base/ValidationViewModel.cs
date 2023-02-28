using MathCore.WPF.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.ViewModels.Base
{
    public class ValidationViewModel : ViewModel, INotifyDataErrorInfo
    {
        protected readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();
        private bool _isEnabledCommand = true;

        public bool IsEnabledCommand
        {
            get { return _isEnabledCommand; }
            set { Set(ref _isEnabledCommand, value); }
        }

        public bool HasErrors => _propertyErrors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public virtual IEnumerable GetErrors(string? propertyName)
        {
            return _propertyErrors.GetValueOrDefault(propertyName, null);
        }

        public virtual void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            IsEnabledCommand = !HasErrors;
        }

        protected virtual void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
    }
}
